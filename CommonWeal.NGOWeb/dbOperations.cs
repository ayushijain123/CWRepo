using CommonWeal.NGOWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

using System.Net.Mail;
using System.Net;
using CommonWeal.NGOWeb.ViewModel;
using CommonWeal.Data;
namespace CommonWeal.NGOWeb
{
    public class dbOperations
    {
        CommonWealEntities context = new CommonWealEntities();
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
        public ForgotPassword UserDetail(string FinalOTP)
        {
            ForgotPassword ob = new ForgotPassword();
            ob = context.ForgotPasswords.Where(w => w.OTP == FinalOTP).FirstOrDefault();

            return ob;
        }
        public List<RegisteredUser> RegisteredUserIsAccepted()
        {
            List<RegisteredUser> userList = new List<RegisteredUser>();
            // userList = context.RegisteredUsers.ToList();
            userList = context.RegisteredUsers.Include(x => x.User).Where(w => w.User.IsActive == true).ToList();
            return userList;
        }
        public List<NGOUser> RegisteredNGOIsAccepted()
        {
            List<NGOUser> userList = new List<NGOUser>();
            // userList = context.RegisteredUsers.ToList();
            userList = context.NGOUsers.Include(x => x.User).Where(w => w.User.IsActive == true).ToList();
            return userList;
        }
        public List<User> UserIsAccepted()
        {
            List<User> userList = new List<User>();
            // userList = context.RegisteredUsers.ToList();
            userList = context.Users.Where(w => w.IsActive == true).ToList();
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
        public string SendActivationEmail(string UserEmail, string Randomcode)//int userId it should be used in the brackets
        {
            //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string activationCode = Guid.NewGuid().ToString();

            var fromAddress = new MailAddress("poojapandey8284@Gmail.com");
            var fromPassword = "dienotattitude";
            var toAddress = new MailAddress(UserEmail);
            try
            {


                string subject = "Fassword Change";
                string body = Randomcode;

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = Randomcode
                })


                    smtp.Send(message);
            }
            catch (Exception ex)
            {

                // Response.Write("Exception in sendEmail:" + ex.Message);
            }

            return activationCode;


        }
        /*Getting set of Post in  List form */
        public List<Post> GetAllPost(int pageNum = 0)
        {
            /*Make List of custom post model type */
            List<Post> ob = new List<Post>();
            /*Getting list from all table */
            var NGOPostlist = context.NGOPosts.OrderByDescending(x => x.CreatedOn).Skip(pageNum * 5).Take(5);
            var RegUserlist = context.RegisteredUsers.ToList();
            var Commentlist = context.PostComments.ToList();
            var NGOUserlist = context.NGOUsers.ToList();
            var LoginUserlist = context.Users.ToList();
            var PostLikeListmain = context.PostLikes.ToList();
            if (NGOPostlist != null)
            {


                foreach (var item in NGOPostlist)
                {

                    // var RegUser =RegUserlist.Where(regusr=>regusr.UserEmail==item.EmailID);
                    var Comment = Commentlist.Where(comment => comment.PostID == item.PostID);


                    Post pm = new Post();
                    pm.userId = item.LoginID.Value;

                    int UserRole = LoginUserlist.Where(user => user.LoginID == item.LoginID).Select(x => x.LoginUserType).FirstOrDefault();

                    switch (UserRole)
                    {
                        case 1: string NGOUser = NGOUserlist.Where(ngusr => ngusr.LoginID == item.LoginID).FirstOrDefault().NGOName.ToString();
                            pm.userName = NGOUser;
                            break;
                        case 3: var RegUser = RegUserlist.Where(lgnuser => lgnuser.LoginID == item.LoginID).FirstOrDefault();
                            pm.userName = RegUser.FirstName + " " + RegUser.LastName;
                            break;

                    }
                    pm.postcontent = item.PostContent;
                    pm.postImageUrl = item.PostUrl;
                    pm.postCreateTime = item.CreatedOn.Value;
                    pm.likeCount = item.PostLikeCount.Value;
                    pm.commentCount = item.PostCommentCount.Value;
                    pm.postId = item.PostID;
                    /*getting list of like user of curent post*/
                    var postcomment = Commentlist.Where(Cmntlst => Cmntlst.PostID == item.PostID).ToList();

                    //start like list
                    List<PostLikeModel> imageLikeList = new List<PostLikeModel>();
                    var PostLikeList = PostLikeListmain.Where(pstlike => pstlike.PostID == item.PostID).ToList();

                    int likecount = 0;
                    foreach (var like in PostLikeList)
                    {

                        PostLikeModel pl = new PostLikeModel();
                        int userType = LoginUserlist.Where(user => user.LoginID == like.LoginID).FirstOrDefault().LoginUserType;

                        switch (userType)
                        {
                            case 1: string NGOUser = NGOUserlist.Where(ngusr => ngusr.LoginID == like.LoginID).FirstOrDefault().NGOName.ToString();
                                pl.userName = NGOUser;
                                break;
                            case 3: var RegUser = RegUserlist.Where(lgnuser => lgnuser.LoginID == like.LoginID).FirstOrDefault();
                                pl.userName = RegUser.FirstName + " " + RegUser.LastName;
                                break;

                        }
                        pl.userImageUrl = "";
                        pl.UserID = like.LoginID;

                        imageLikeList.Add(pl);
                        likecount++;

                    }
                    pm.likeCount = item.PostLikeCount.Value;
                    pm.postlike = imageLikeList;
                    //end like list

                    // start all comment of  particular post
                    List<Comment> imagecommentlist = new List<Comment>();
                    pm.commentCount = postcomment.Count();
                    int commentcount = 0;
                    foreach (var a in postcomment)
                    {
                        Comment cmnt = new Comment();
                        cmnt.commentId = a.CommentID;
                        cmnt.commentContent = a.CommentText;
                        cmnt.commentLike = 0;
                        cmnt.commentUserImage = "";
                        cmnt.CreatedDateTime = a.CreatedOn.Value;
                        int userType = LoginUserlist.Where(user => user.LoginID == a.LoginID).FirstOrDefault().LoginUserType;

                        switch (userType)
                        {
                            case 1: string NGOUser = NGOUserlist.Where(ngusr => ngusr.LoginID == a.LoginID).FirstOrDefault().NGOName.ToString();
                                cmnt.Username = NGOUser;
                                break;
                            case 3: var RegUser = RegUserlist.Where(lgnuser => lgnuser.LoginID == a.LoginID).FirstOrDefault();
                                cmnt.Username = RegUser.FirstName + " " + RegUser.LastName;
                                break;

                        }

                        imagecommentlist.Add(cmnt);
                        commentcount++;
                    }
                    pm.commentCount = item.PostCommentCount.Value;
                    //end all comment of  particular post
                    pm.PostComments = imagecommentlist;
                    ob.Add(pm);

                }
            }
            return (ob);
        }


        public List<RegisteredUser> All_Users()
        {
            List<RegisteredUser> userList = new List<RegisteredUser>();
            // userList = context.RegisteredUsers.ToList();
            userList = context.RegisteredUsers.Include(x => x.User).Where(w => w.User.IsActive == true).ToList();
            return userList;
        }
        public List<RegisteredUser> Blocked_NormalUsers()
        {
            List<RegisteredUser> userList = new List<RegisteredUser>();
            userList = context.RegisteredUsers.Include(x => x.User).Where(w => w.User.IsBlock == true).ToList();
            return userList;
        }

    }
}

