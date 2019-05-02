using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.BackgroundJobs;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.Mail;
using DataAccess.Abstract;
using Entities.Concrete;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        IBackgroundWorker _backgroundWorker;
        IMailService _mailService;

        public ProductManager(IProductDal productDal, IBackgroundWorker backgroundWorker, IMailService mailService)
        {
            _productDal = productDal;
            _backgroundWorker = backgroundWorker;
            _mailService = mailService;
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
           
            return _productDal.Get(filter);
        }

        [CacheInterceptionAspect]
        public List<Product> GetList(Expression<Func<Product, bool>> filter = null)
        {
            //Thread.Sleep(3000);
            _backgroundWorker.Schedule(() => _mailService.Send(new Mail()), TimeSpan.FromMinutes(1));
            return _productDal.GetList(filter).ToList();

        }

        

        [ValidationInterceptionAspect(typeof(ProductValidation))]
        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }

        public void DeleteById(int id)
        {
            var product = Get(p => p.ProductId == id);
            _productDal.Delete(product);
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }
    }
}
