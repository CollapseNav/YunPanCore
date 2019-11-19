// ***********************************************************************
// Assembly         : OpenAuth.Mvc
// Author           : yubaolee
// Created          : 10-26-2015
//
// Last Modified By : yubaolee
// Last Modified On : 10-26-2015
// ***********************************************************************
// <copyright file="AutofacExt.cs" company="www.cnblogs.com/yubaolee">
//     Copyright (c) www.cnblogs.com/yubaolee. All rights reserved.
// </copyright>
// <summary>IOC扩展</summary>
// ***********************************************************************

using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using IContainer = Autofac.IContainer;
using Dal.Interface;
using Dal;

namespace Bll
{
    public static  class AutofacExt
    {
        private static IContainer _container;
        public static IContainer InitAutofac(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
           
            //注册数据库基础操作和工作单元
            services.AddScoped(typeof(IBaseDal<>), typeof(BaseDal<>));

            //注册app层
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            builder.Populate(services);

            _container = builder.Build();
            return _container;

        }

        /// <summary>
        /// 从容器中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T GetFromFac<T>()
        {
            return _container.Resolve<T>();
        }

        public static object GetFromFac(Type type)
        {
            return _container.Resolve(type);
        }

    }
}