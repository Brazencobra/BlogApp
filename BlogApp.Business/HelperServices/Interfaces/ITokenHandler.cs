﻿using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.HelperServices.Interfaces
{
    public interface ITokenHandler
    {
        TokenResponseDto CreateToken(AppUser user , int expires = 300);
        string CreateRefreshToken();
    }
}
