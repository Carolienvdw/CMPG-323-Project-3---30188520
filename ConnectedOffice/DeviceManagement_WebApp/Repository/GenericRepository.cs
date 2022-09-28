using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using DeviceManagement_WebApp.Data;
using System.Linq;
using DeviceManagement_WebApp.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DeviceManagement_WebApp.Repository
{
    //The GenericRepositorythat implements the IGenericRepository.
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ConnectedOfficeContext _context;
        public GenericRepository(ConnectedOfficeContext context)
        {
            _context = context;
        }

        //This adds a new entity
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            
        }

        //This edits a entiry
        public void Edit(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        //This adds a new range to an entity
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        //This search if a expression is true
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        // This retrieves all of the same class
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        //This gets an entity by its id
        public T GetById(Guid? id)
        {
            return _context.Set<T>().Find(id);
        }

        //This deletes an entity
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        //This removes a range from an entity
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        
    }
}


