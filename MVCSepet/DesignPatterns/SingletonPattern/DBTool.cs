using MVCSepet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSepet.DesignPatterns.SingletonPattern
{
    public class DBTool
    {


        DBTool() { }

        static NorthwindEntities _dbInstance;

        public static NorthwindEntities DBInstance
        {

            get
            {
                if (_dbInstance == null)
                {
                    _dbInstance = new NorthwindEntities();
                }
                return _dbInstance;
            }

        }



    }
}