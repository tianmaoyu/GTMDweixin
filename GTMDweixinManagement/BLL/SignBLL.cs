using GTMDweixinManagement.EF;
using LinqKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace GTMDweixinManagement.BLL
{
    public class SignBLL
    {
        private GTMDweixinEntities db = null;
        public SignBLL()
        {
            db = new GTMDweixinEntities();
        }

        public JObject GetInfo(int id)
        {
            JObject result = new JObject();
            if (id > 0)
            {
                var info = db.SignInfoes.Where(item => item.ID == id).FirstOrDefault();
                if (info != null)
                {
                    result = JObject.Parse(JsonConvert.SerializeObject(info));
                }
            }
            return result;
        }


        public JArray GetTree()
        {
            JArray jArray = new JArray();
            var listInfos = db.SignInfoes.Select(item=>new {
                id=item.ID,
                text=item.DisplayName,
                state="open",
            }).ToList();
            JArray children = JArray.Parse(JsonConvert.SerializeObject(listInfos));
            JObject rootNode = new JObject();
            rootNode["id"] = -2;
            rootNode["text"] = "全部签到";
            rootNode["state"] = "open";
            rootNode["children"] = children;
            jArray.Add(rootNode);
            return jArray;
        }



        //新增
        public int Add(JObject json)
        {
            SignInfo signInfo = JsonConvert.DeserializeObject<SignInfo>(json.ToString());
            db.Insert(signInfo);
            return 1;
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Updata(JObject json, int id)
        {
            SignInfo signInfo = JsonConvert.DeserializeObject<SignInfo>(json.ToString());
            signInfo.ID = id;
            var _signInfo = db.SignInfoes.AsNoTracking().FirstOrDefault(item => item.ID == id);
            if (_signInfo != null)
            {
                db.Updata(signInfo);
            }
            return 1;
        }
        public List<SignInfo> GetAll()
        {
            return db.SignInfoes.ToList();
        }

        public int Delete(int id)
        {
            var signInfo = db.SignInfoes.FirstOrDefault(item => item.ID == id);
            if (signInfo != null)
            {
                db.Delete(signInfo);
            }
            return 1;
        }

        /// <summary>
        /// 得到可以勾班的列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<SignInfo> GetUseableSign(System.Net.Http.HttpRequestMessage request)
        {
            var infos = GetCurrentStudentSign(request);
            if (infos.Any())
            {
               return infos.Where(item => item.IsActive == true).ToList();
            }
            return null;
        }

        /// <summary>
        /// 得到当前学生已经勾的勾班列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<SignInfo> GetExpireSign(System.Net.Http.HttpRequestMessage request)
        {
            var infos = GetCurrentStudentSign(request);
            if (infos.Any())
            {
                return infos.Where(item => item.IsActive == false).ToList();
            }
            return null;
        }

        /// <summary>
        /// 得到当前的学生已经勾班的，和可以勾班的，勾班列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private List<SignInfo> GetCurrentStudentSign(System.Net.Http.HttpRequestMessage request)
        {
            ProjectBLL projectBll = new ProjectBLL();
            var projects = projectBll.GetEnterProject(request);
            if (projects.Any())
            {
                var ids = projects.Select(item => item.ID);
                return db.SignInfoes.Where(item => ids.Contains((int)item.ProjectID)).ToList();
            }
            return null;
        }
    }
}