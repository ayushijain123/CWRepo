﻿using CommonWeal.NGOWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using CommonWeal.NGOWeb.Models;
using System.Net.Mail;
using System.Net;
using CommonWeal.NGOWeb.ViewModel;
namespace CommonWeal.NGOWeb
{
    public class dbOperations
    {
        CommonWealEntities1 context = new CommonWealEntities1();
        public List<NGOUser> GetAllUserNotAccepted()
        {
            List<NGOUser> userList = new List<NGOUser>();
            userList = context.NGOUsers.Where(w => w.IsActive == false && w.IsBlock == false).ToList();
            return userList;
        }
        public List<NGOUser> GetAllUserAccepted()
        {
            List<NGOUser> userList = new List<NGOUser>();
            userList = context.NGOUsers.Where(w => w.IsActive == true).ToList();
            //userList = context.NGOUsers.Include(x => x.User).Where(w => w.User.IsActive== false).ToList();
            return userList;
        }
        public List<NGOUser> GetAllUserBlocked()
        {
            List<NGOUser> userList = new List<NGOUser>();
            userList = context.NGOUsers.Where(w => w.IsBlock == true).ToList();
            return userList;
        }
        public NGOUser GetNGODetails(int id)
        {
            NGOUser ob = new NGOUser();
            ob = context.NGOUsers.Where(w => w.LoginID == id).FirstOrDefault();

            return ob;
        }
        public List<RegisteredUser> RegisteredUserIsAccepted()
        {
            List<RegisteredUser> userList = new List<RegisteredUser>();
            // userList = context.RegisteredUsers.ToList();
            userList = context.RegisteredUsers.Include(x => x.User).Where(w => w.User.IsActive == true).ToList();
            return userList;
        }
        public string GenerateRandomPassword(int length)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-*&#+";
            char[] chars = new char[length];
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }
        public List<Post> GetAllPost()
        {

            List<Post> ob = new List<Post>();
            var NGOPostlist = context.NGOPosts.ToList();
            var RegUserlist = context.RegisteredUsers.ToList();
            var Commentlist = context.PostComments.ToList();
            var NGOUserlist = context.NGOUsers.ToList();
            var LoginUserlist = context.Users.ToList();

            foreach (var item in NGOPostlist)
            {

                // var RegUser =RegUserlist.Where(regusr=>regusr.UserEmail==item.EmailID);
                var Comment = Commentlist.Where(comment => comment.PostID == item.PostID);


                Post pm = new Post();

                pm.userName = "";
                int UserRole = LoginUserlist.Where(user => user.LoginEmailID == item.EmailID).Select(x => x.LoginUserType).FirstOrDefault();

                switch (UserRole)
                {
                    case 1: String NGOUser = NGOUserlist.Where(ngusr => ngusr.NGOEmailID == item.EmailID).Select(ngusr => ngusr.NGOName).ToString();
                        pm.userName = NGOUser;
                        break;
                    case 3: var RegUser = RegUserlist.Where(lgnuser => lgnuser.UserEmail == item.EmailID).FirstOrDefault();
                        pm.userName = RegUser.FirstName + " " + RegUser.LastName;
                        break;


                }
                pm.postImageUrl = item.PostUrl;
                pm.postCreateTime = item.CreatedOn.Value;
                pm.likeCount = 1;//item.PostLikeCount.Value;
                pm.commentCount = 1;//item.PostCommentCount.Value;

                var postcomment = Commentlist.Where(Cmntlst => Cmntlst.PostID == item.PostID).ToList();


                ob.Add(pm);

            }
            return (ob);
        }
    }
}