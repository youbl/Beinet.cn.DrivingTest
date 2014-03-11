using System.Runtime.Serialization;

namespace DrivingTest
{
    [DataContract]
    internal class Question
    {
        // DataMember属性是用于反序列化站点返回的问题

        /// <summary>
        /// 问题id
        /// </summary>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public int id { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string question { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string a { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string b { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string c { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string d { get; set; }
        /// <summary>
        /// 正确答案序号
        /// </summary>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public int ta { get; set; }
        /// <summary>
        /// 题目用到的图片（可能是swf）
        /// </summary>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string imageurl { get; set; }
        /// <summary>
        /// 答案解答
        /// </summary>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string bestanswer { get; set; }
        /// <summary>
        /// 答案解答网页连接id，"http://tieba.jxedt.com/posts_" + bestanswerid + ".html"
        /// </summary>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string bestanswerid { get; set; }
        /// <summary>
        /// 等于1表示判断题，等于2表示4选1，等于3表示4选多，多选题
        /// </summary>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string Type { get; set; }
        /// <summary>
        /// 新浪图片
        /// </summary>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string sinaimg { get; set; }

        // 以下属性用于考试窗体
        /// <summary>
        /// 是否选择题
        /// </summary>
        public bool IsOption { get; set; }
        //{
        //    get
        //    {
        //        return string.IsNullOrEmpty(c) && string.IsNullOrEmpty(d);
        //    } 
        //}

        /// <summary>
        /// 学员选择的答案
        /// </summary>
        public int Answer { get; set; }

        /// <summary>
        /// 回答是否正确
        /// </summary>
        public bool IsRight
        {
            get
            {
                return Answer == ta;
            }
            
        }
    }
}
