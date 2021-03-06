﻿using System;
using System.Collections.Generic;
using System.Linq;
using GFT_ClubHouse__Management.Data;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace GFT_ClubHouse__Management.Repositories {
    public class MusicalGenreRepository : IMusicalGenreRepository {
        private readonly ApplicationDbContext _dbContext;

        public MusicalGenreRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public int Count() {
            return _dbContext.Set<MusicalGenre>().Count();
        }

        public bool Exists(int id) {
            return _dbContext.Set<MusicalGenre>().Any(x => x.Id.Equals(id));
        }

        public IEnumerable<MusicalGenre> GetAll() {
            return _dbContext.Set<MusicalGenre>().AsNoTracking().ToList();
        }

        public IPagedList<MusicalGenre> GetAll(int? page, string search) {
            var pageNumber = page ?? 1;
            const int resultsPerPage = 10;

            if (string.IsNullOrEmpty(search))
                return _dbContext.Set<MusicalGenre>().ToPagedList(pageNumber, resultsPerPage);

            search = search.Trim().ToLower();
            return _dbContext.Set<MusicalGenre>()
                .Where(t => t.Name.ToLower().Contains(search))
                .ToPagedList(pageNumber, resultsPerPage);
        }

        public MusicalGenre GetById(object id) {
            return _dbContext.Set<MusicalGenre>().AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
        }

        public List<SelectListItem> GetSelectList() {
            return _dbContext.Set<MusicalGenre>()
                .Select(x => new SelectListItem() {Value = x.Id.ToString(), Text = x.Name}).AsNoTracking().ToList();
        }

        public List<MusicalGenre> GetAllByName(string name) {
            return _dbContext.Set<MusicalGenre>().AsNoTracking()
                .Where(x => x.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public void Insert(MusicalGenre obj) {
            _dbContext.Set<MusicalGenre>().Add(obj);
            Save();
        }

        public void Update(MusicalGenre obj) {
            _dbContext.Set<MusicalGenre>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id) {
            var existing = _dbContext.Set<MusicalGenre>().Find(id);
            _dbContext.Set<MusicalGenre>().Remove(existing);
            Save();
        }

        public void Save() {
            _dbContext.SaveChanges();
        }
    }
}