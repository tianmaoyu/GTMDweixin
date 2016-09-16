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
    public class ProjectBLL
    {
        private GTMDweixinEntities db = null;
        public ProjectBLL()
        {
            db = new GTMDweixinEntities();
        }

        public JObject GetInfo(int id)
        {
            JObject result = new JObject();
            if (id > 0)
            {
                var info = db.ProjectInfoes.Where(item => item.ID == id).FirstOrDefault();
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
            var listInfos = db.ProjectInfoes.Select(item=>new {
                id=item.ID,
                text=item.DisplayName,
                state="open",
            }).ToList();
            JArray children = JArray.Parse(JsonConvert.SerializeObject(listInfos));
            JObject rootNode = new JObject();
            rootNode["id"] = -2;
            rootNode["text"] = "全部菜单";
            rootNode["state"] = "open";
            rootNode["children"] = children;
            jArray.Add(rootNode);
            return jArray;
        }



        //新增
        public int Add(JObject json)
        {
            ProjectInfo projectInfo = JsonConvert.DeserializeObject<ProjectInfo>(json.ToString());
            db.Insert(projectInfo);
            return 1;
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Updata(JObject json, int id)
        {
            ProjectInfo projectInfo = JsonConvert.DeserializeObject<ProjectInfo>(json.ToString());
            projectInfo.ID = id;
            var _ProjectInfo = db.ProjectInfoes.AsNoTracking().FirstOrDefault(item => item.ID == id);
            if (projectInfo != null)
            {
                db.Updata(projectInfo);
            }
            return 1;
        }
        public List<ProjectInfo> GetAll()
        {
            return db.ProjectInfoes.ToList();
        }

        public int Delete(int id)
        {
            var ProjectInfo = db.ProjectInfoes.FirstOrDefault(item => item.ID == id);
            if (ProjectInfo != null)
            {
                db.Delete(ProjectInfo);
            }
            return 1;
        }

    }
}