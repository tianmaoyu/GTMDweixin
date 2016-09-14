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
    public class MenuBLL
    {
        private GTMDweixinEntities db = null;
        public MenuBLL()
        {
            db = new GTMDweixinEntities();
        }

        public JObject GetInfo(int id)
        {
            JObject result = new JObject();
            if (id > 0)
            {
              var info=  db.MeunInfoes.Where(item => item.ID == id).FirstOrDefault();
                if (info != null)
                {
                    result= JObject.Parse( JsonConvert.SerializeObject(info));
                }
            }
            return result;
        }


        public JArray GetTree(int? isSelectedID=1)
        {
            isSelectedID = isSelectedID ?? 0;
            JArray jArray = new JArray();
            if (isSelectedID == 0)
            {
                JObject rootNode = new JObject();
                rootNode["id"] = -2;
                rootNode["text"] = "全部菜单";
                rootNode["state"]="open";
                rootNode["children"] = CreateTree(-2);
                jArray.Add(rootNode);
            }
            return jArray;
        }


        /// <summary>
        /// 递归tree
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        private JArray CreateTree(int? parentID)
        {
            parentID = parentID ?? -2;
            JArray jArray = new JArray();
            var listInfos=db.MeunInfoes.Where(item => item.TreeParentID == parentID).ToList();

            //退出递归
            if (listInfos.Count <= 0) return null;
        
            foreach (MeunInfo info in listInfos)
            {
                JObject node = new JObject();
                node["id"] = info.ID;
                node["text"] = info.DisplayName;
                node["state"] = "open";
                node["children"] = CreateTree(info.ID);
                jArray.Add(node);
            }
            return jArray;
        }

        //新增
        public int Add(JObject json)
        {
            MeunInfo userInfo = JsonConvert.DeserializeObject<MeunInfo>(json.ToString());
            db.Insert(userInfo);
            return 1;
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Updata(JObject json, int id)
        {
            MeunInfo userInfo = JsonConvert.DeserializeObject<MeunInfo>(json.ToString());
            userInfo.ID = id;
            var _meunInfo = db.MeunInfoes.AsNoTracking().FirstOrDefault(item=>item.ID==id);
            userInfo.TreeParentID = _meunInfo.TreeParentID;
            if (userInfo != null)
            {
                db.Updata(userInfo);
            }
            return 1;
        }

        public int Delete(int id)
        {
            var meunInfo = db.MeunInfoes.FirstOrDefault(item => item.ID == id);
            if (meunInfo != null)
            {
                db.Delete(meunInfo);
            }
            return 1;
        }
       
    }
}