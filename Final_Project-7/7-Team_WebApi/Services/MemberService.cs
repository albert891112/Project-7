using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Repositories;
using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace _7_Team_WebApi.Services
{
    public class MemberService : IService<MemberDTO>
    {
        MemberRepository repo = new MemberRepository();

        /// <summary>
        /// Get member data by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MemberDTO Get(int id)
        {
            MemberDTO dto = this.repo.Get(id).ToDTO();

            return dto;
        }

        /// <summary>
        /// Get all member data
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<MemberDTO> GetAll()
        {
            List<MemberEntity> entities = this.repo.GetAll();

            List<MemberDTO> dtos = entities.Select(x => x.ToDTO()).ToList();

            return dtos;
        }
    
        

        /// <summary>
        /// Create new member
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Create(MemberDTO dto)
        {
            MemberEntity entity = dto.ToEntity();

            this.repo.Create(entity);
        }


        /// <summary>
        /// Delete member by id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        
        /// <summary>
        /// Update member data
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update(MemberDTO dto)
        {
            MemberEntity entity = dto.ToEntity();

            this.repo.Update(entity);
        }
    }
}