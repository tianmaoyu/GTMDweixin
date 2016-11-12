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
            int signID = value["signID"].Value<int>();
            value.Remove("signID");
            EF.StudentSignInfo info = new EF.StudentSignInfo();
            var studentInfo= new StudentBLL().GetCurrentStudent(Request);
            if (studentInfo != null&& signID >-1 )
            {
               var jarray=  JArray.Parse(value.ToString());
               
                var list = jarray.ToObject<List<Tuple<string, bool>>>();
                info.FirstDay_Morning = list[0].Item2;
                info.FirstDay_Afternoon = list[1].Item2;
                info.FifthDay_Evening = list[3].Item2;

                info.SecondDay_Morning = list[4].Item2;
                info.SecondDay_Afternoon = list[5].Item2;
                info.SecondDay_Evening = list[6].Item2;

                info.ThirdDay_Morning = list[7].Item2;
                info.ThirdDay_Afternoon = list[8].Item2;
                info.ThirdDay_Evening = list[9].Item2;

                info.FourthDay_Morning = list[10].Item2;
                info.FourthDay_Afternoon = list[11].Item2;
                info.FourthDay_Evening = list[12].Item2;

                info.FifthDay_Morning = list[13].Item2;
                info.FifthDay_Afternoon = list[14].Item2;
                info.FifthDay_Evening = list[15].Item2;

                info.SixthDay_Morning = list[16].Item2;
                info.SixthDay_Afternoon = list[17].Item2;
                info.FifthDay_Evening = list[18].Item2;

                info.SeventhDay_Morning = list[19].Item2;
                info.SeventhDay_Afternoon = list[20].Item2;
                info.SeventhDay_Evening = list[21].Item2;
            }
            return null;
        }
    }
}
