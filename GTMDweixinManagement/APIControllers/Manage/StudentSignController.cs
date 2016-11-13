using GTMDweixinManagement.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GTMDweixinManagement.APIControllers
{
    public class StudentSignController : ApiController
    {
        public object StudentSignBLL { get; private set; }

        // Post: api/User/GetList
        [HttpPost]
        public JObject GetList(JObject pagerParas)
        {
            StudentSignBLL studentSignBLL = new StudentSignBLL();
            return studentSignBLL.GetList(pagerParas);
        }

        // Post: api/User/Add
        [HttpPost]
        public string Add(JObject pagerParas)
        {
            var id = HttpContext.Current.Request.QueryString["id"];
            //var form = HttpContext.Current.Request.Form;

            StudentSignBLL studentSignBLL = new StudentSignBLL();
            if (id != null && Int32.Parse(id) > 0)
            {
                studentSignBLL.Updata(pagerParas, Int32.Parse(id));
            }
            else
            {
                try
                {
                    pagerParas.Remove("ID");
                }
                catch (Exception ex)
                {

                }
                studentSignBLL.Add(pagerParas);
            }
            return "1";
        }

        // DELETE: api/User/Delete/5
        [HttpPost]
        public JObject Delete(JObject json)
        {
            var ids = JsonConvert.DeserializeObject<List<int>>(json["ids"].ToString());
            StudentSignBLL studentSignBLL = new StudentSignBLL();
            var deleteSuccess = studentSignBLL.Delete(ids);
            JObject result = new JObject();
            if (deleteSuccess == 1)
            {
                result["success"] = true;
            }
            else
            {
                result["errorMsg"] = "删除失败！";
            }
            return result;
        }

        public JArray GetProject()
        {
            //new StudentProjectBLL()
            return new JArray();
        }

        public JObject Edit(JObject pagerParas)
        {
            StudentSignBLL studentSignBLL = new StudentSignBLL();
            studentSignBLL.Add(pagerParas);
            return new JObject();
        }

        /// <summary>
        /// 微信端提交勾班数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JObject Greate(JObject value)
        {
            JObject reslut = new JObject();
            int signID = value["signID"].Value<int>();
            value.Remove("signID");
            EF.StudentSignInfo info = new EF.StudentSignInfo();
            var studentInfo = new StudentBLL().GetCurrentStudent(Request);
            StudentSignBLL bll = new StudentSignBLL();
            if (studentInfo != null && signID > -1)
            {
                //如果已经保存过了则直接返回
                if (bll.Find(item => item.SignID == signID && item.StudentID == studentInfo.ID).Any())
                {
                    reslut["msg"] = "保存无效，不能再许改！";
                    return reslut;
                }
                var list = value["list"].ToObject<List<bool>>();
                info.FirstDay_Morning = list[0];
                info.FirstDay_Afternoon = list[1];
                info.FirstDay_Evening = list[2];

                info.SecondDay_Morning = list[3];
                info.SecondDay_Afternoon = list[4];
                info.SecondDay_Evening = list[5];

                info.ThirdDay_Morning = list[6];
                info.ThirdDay_Afternoon = list[7];
                info.ThirdDay_Evening = list[8];

                info.FourthDay_Morning = list[9];
                info.FourthDay_Afternoon = list[10];
                info.FourthDay_Evening = list[11];

                info.FifthDay_Morning = list[12];
                info.FifthDay_Afternoon = list[13];
                info.FifthDay_Evening = list[14];

                info.SixthDay_Morning = list[15];
                info.SixthDay_Afternoon = list[16];
                info.SixthDay_Evening = list[17];

                info.SeventhDay_Morning = list[18];
                info.SeventhDay_Afternoon = list[19];
                info.SeventhDay_Evening = list[20];
                info.SignID = signID;
                info.StudentID = studentInfo.ID;
                info.CreateTime = DateTime.Now;
                if (bll.Add(info) == 1)
                {
                    reslut["status"] = true;
                    return reslut;
                }
                reslut["msg"] = "保存出错！";
                return reslut;
            }
            return null;
        }

        public JArray GetSignInfo(int signID)
        {
            var studentInfo = new StudentBLL().GetCurrentStudent(Request);
            StudentSignBLL bll = new StudentSignBLL();
            if (studentInfo != null && signID > -1)
            {
                //如果已经保存过了则直接返回
                var info = bll.Find(item => item.SignID == signID && item.StudentID == studentInfo.ID).FirstOrDefault();
                if (info!=null)
                {
                    List<bool?> list = new List<bool?>();
                    list.Add(info.FirstDay_Morning);
                    list.Add(info.FirstDay_Afternoon);
                    list.Add(info.FirstDay_Evening);

                    list.Add(info.SecondDay_Morning);
                    list.Add(info.SecondDay_Afternoon);
                    list.Add(info.SecondDay_Evening);

                    list.Add(info.ThirdDay_Morning);
                    list.Add(info.ThirdDay_Afternoon);
                    list.Add(info.ThirdDay_Evening );

                    list.Add(info.FourthDay_Morning);
                    list.Add(info.FourthDay_Afternoon);
                    list.Add(info.FourthDay_Evening);

                    list.Add(info.FifthDay_Morning);
                    list.Add(info.FifthDay_Afternoon);
                    list.Add(info.FifthDay_Evening);

                    list.Add(info.SixthDay_Morning);
                    list.Add(info.SixthDay_Afternoon) ;
                    list.Add(info.SixthDay_Evening );

                    list.Add(info.SeventhDay_Morning) ;
                    list.Add(info.SeventhDay_Afternoon );
                    list.Add(info.SeventhDay_Evening);
                    return JArray.Parse(JsonConvert.SerializeObject(list));
                }
                return null;
            }
            return null;
        }
    }
}
