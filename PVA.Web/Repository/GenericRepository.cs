﻿using PVA.Web.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace PVA.Web.Repository
{
    public class GenericRepository<T> where T : class
    {
        /// <summary>
        /// Function to get entites
        /// </summary>
        /// <returns>DbSet result of entities</returns>
        public DbSet<T> GetEntities()
        {
            PhysicalVerification_HUL_NewEntities context = BaseContext.GetDbContext();
            return context.Set<T>();
        }

        /// <summary>
        /// Function to create entity instance
        /// </summary>
        /// <returns>New instance of entity</returns>
        public T CreateEntity()
        {
            using (PhysicalVerification_HUL_NewEntities context = BaseContext.GetDbContext())
            {
                DbSet<T> table = context.Set<T>();
                return table.Create();
            }
        }

        /// <summary>
        /// Function to select entity by primary key
        /// </summary>
        /// <param name="pk">Primary key of entity to fetch</param>
        /// <returns></returns>
        public T SelectById(object pk)
        {
            using (PhysicalVerification_HUL_NewEntities context = BaseContext.GetDbContext())
            {
                DbSet<T> table = context.Set<T>();
                return table.Find(pk);
            }
        }

        /// <summary>
        /// Function to insert entity.
        /// </summary>
        /// <param name="entity">entity to be inserted</param>
        public string Insert(T entity)
        {
            try
            {
                using (PhysicalVerification_HUL_NewEntities context = BaseContext.GetDbContext())
                {
                    DbSet<T> table = context.Set<T>();
                    table.Add(entity);
                    context.SaveChanges();
                    return string.Empty;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                string messages = String.Empty;
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        //Console.WriteLine("Error Property Name {0} : Error Message: {1}",
                        //                    error.PropertyName, error.ErrorMessage);
                        messages = messages + "<br/>" + string.Join("<br/>", error.ErrorMessage);

                    }
                }
                return messages;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        /// <summary>
        /// Function to update entity.
        /// </summary>
        /// <param name="entity">entity to be updated</param>
        public string Update(T entity)
        {
            try
            {
                using (PhysicalVerification_HUL_NewEntities context = BaseContext.GetDbContext())
                {
                    DbSet<T> table = context.Set<T>();
                    table.Attach(entity);
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pk">Primary key of entity to be deleted</param>
        public string Delete(object pk)
        {
            try
            {
                using (PhysicalVerification_HUL_NewEntities context = BaseContext.GetDbContext())
                {
                    DbSet<T> table = context.Set<T>();
                    T existing = table.Find(pk);
                    context.Entry(existing).State = EntityState.Deleted;
                    context.SaveChanges();
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}