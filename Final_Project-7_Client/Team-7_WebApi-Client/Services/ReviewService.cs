using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Repositories;

namespace Team_7_WebApi_Client.Services
{
    public class ReviewService
    {
        ReviewRepository repo = new ReviewRepository();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public List<ReviewDTO> Get(int ProductId)
        {
        
            List<ReviewEntity> entities = this.repo.Get(ProductId);

            List<ReviewDTO> dtos = entities.Select(e => e.ToDTO()).ToList();

            return dtos;
        }
   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public OrderDTO CheckOrderItemExist(int ProductId)
        {
            var MemberRepo = new MemberRepository();

            var Account = HttpContext.Current.User.Identity.Name;
            var MemberId = MemberRepo.GetIdByAccount(Account);

            var order = this.repo.GetOrderItem(ProductId, MemberId);

            OrderDTO dto = order.ToDTO();   

            return dto;


        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="Exception"></exception>
        public void Create(ReviewDTO dto)
        {
            var MemberRepo = new MemberRepository();

            var Account = HttpContext.Current.User.Identity.Name;   
            var MemberId = MemberRepo.GetIdByAccount(Account);

            var order = this.repo.GetOrderItem(dto.ProductId, MemberId);

            if(order == null)
            {
                throw new Exception("You have not purchased this product");
            }

            ReviewEntity entity = dto.ToEntity();

            entity.ReviewTime = DateTime.Now;
            entity.OrderTime = order.OrderTime;

            this.repo.Create(entity);
        }
    
    
    }
}