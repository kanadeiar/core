﻿using System.Collections.Generic;

namespace WebApplication1.Interfaces.WebAPI
{
    public interface IValuesService
    {
        IEnumerable<string> GetAll();
        string GetById(int id);
        void Add(string str);
        void Edit(int id, string str);
        int Delete(int id);
    }
}
