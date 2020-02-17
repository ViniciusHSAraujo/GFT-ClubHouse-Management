﻿using GFT_ClubHouse__Management.Data;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using GFT_ClubHouse__Management.Libs.Language;
using GFT_ClubHouse__Management.Libs.Security;
using GFT_ClubHouse__Management.Models.Enum;

namespace GFT_ClubHouse__Management.Repositories {
    public class UserRepository : IUserRepository {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }
        
        public int Count() {
            return _dbContext.Set<User>().Count();
        }

        public IEnumerable<User> GetAll() {
            return _dbContext.Set<User>().Include(x => x.Address).AsNoTracking().ToList();
        }

        public User GetById(object id) {
            return _dbContext.Set<User>().Include(x => x.Address).AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
        }

        public List<SelectListItem> GetSelectList() {
            return _dbContext.Set<User>().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).AsNoTracking().ToList();
        }

        public void Insert(User obj) {

            if (IsRegistered(obj.Email, obj.Roles)) {
                throw new VerificationException(ErrorMessages.MSG_E006);
            }
            
            _dbContext.Set<User>().Add(obj);
            Save();
        }

        public User Login(string email, string pass, UserRoles roles) {
            return _dbContext.Set<User>().FirstOrDefault(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase) && x.Password.Equals(pass, StringComparison.InvariantCulture) && x.Roles == roles);
        }

        public bool IsRegistered(string email, UserRoles roles) {
            return _dbContext.Set<User>().Any(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase) && x.Roles == roles);
        }

        public void Update(User obj) {
            _dbContext.Set<User>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.Entry(obj).Property(x => x.Password).IsModified = false;
            Save();
        }

        public void Delete(object id) {

            if (id.Equals(1)) {
                throw new SecurityException("This user cannot be deleted!");
            }
            
            User existing = _dbContext.Set<User>().Find(id);
            _dbContext.Set<User>().Remove(existing);
            
            Save();
        }

        public void Save() {
            _dbContext.SaveChanges();
        }
        
    }
}
