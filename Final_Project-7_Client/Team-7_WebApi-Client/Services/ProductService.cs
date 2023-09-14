using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Repositories;

namespace Team_7_WebApi_Client.Services
{
    public class ProductService
    {
        ProductRepository repo = new ProductRepository();

        public ProductDTO Get(int Id)
        {
            ProductDTO dtos = repo.Get(Id).ToDTO();

            return dtos;
        }


        

    }
}