/*
* =================================================================================
* Project:       BidMatrix/API
* File:          User.cs
* Description:   User model class for db table.
* 
* Revision History:
* Date         Author          Description
* -----------  --------------  ----------------------------------------------------
* 2026-09-30   Keerthan P Poojary   Created user model for creating and manipulating 
                                    the user table in the db.
* ==================================================================================
*/
using API.Models.Enums;
using System;

namespace API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt {  get; set; }

        #region Constructor
        public User()
        {
            
        }
        public User(string username,string email,string passwordhash,UserRole userRole)
        {
           Username = username;
           Email = email;   
           PasswordHash = passwordhash;
           Role = userRole;
           IsActive = true;
           CreatedAt = DateTime.Now;
        }
        #endregion
    }
}
