using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SinhVienController : Controller
    {
        DataClasses1DataContext db=new DataClasses1DataContext();
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public string GetList(string search)
        {
            var dssv = db.SinhViens;

            if (string.IsNullOrEmpty(search))
            {
                return JsonConvert.SerializeObject(dssv);
            }
            else
            {
                // Thực hiện tìm kiếm dựa trên điều kiện search và trả về kết quả tương ứng tìm kiếm theo tên
                var filteredData = dssv.Where(s =>
                            s.HoTen.Contains(search) ||
                            s.MSSV.Contains(search)
                );
                return JsonConvert.SerializeObject(filteredData);
            }
        }
        public string GetObject(string mssv)
        {
            SinhVien sinhvien = new SinhVien();
            sinhvien = db.SinhViens.Where(o => o.MSSV.Equals(mssv)).FirstOrDefault();
            return JsonConvert.SerializeObject(sinhvien);
        }

        [HttpPost]
        public string PostCreate()
        {
            Result_ett<string> rs = new Result_ett<string>();
            try
            {
                string hoTen = Request["HoTen"];
                string mSSV = Request["MSSV"];
                string khoaHoc = Request["Khoa"];
                string lopQuanLy = Request["Lop"];
                string diaChi = Request["DiaChi"];
                string diemTBString = Request["DiemTB"];
                float DiemTB = float.Parse(diemTBString);

                SinhVien newSV = new SinhVien();
                newSV.MSSV = mSSV;
                newSV.HoTen = hoTen;
                newSV.Khoa = khoaHoc;
                newSV.Lop = lopQuanLy;
                newSV.DiaChi = diaChi;
                newSV.DiemTB = DiemTB;
                db.SinhViens.InsertOnSubmit(newSV);
                db.SubmitChanges();
                rs.ErrCode = EnumErrCode.Success;
                rs.Message = "Thêm mới sinh viên thành công";
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Fail;
                rs.ErrDetail = ex.Message;
                rs.Message = "Thêm mới sinh viên không thành công";
                return JsonConvert.SerializeObject(rs);
            }
            return JsonConvert.SerializeObject(rs);
        }
        [HttpPost]
        public string PostEdit()
        {
            Result_ett<string> rs = new Result_ett<string>();
            try
            {
                string hoTen = Request["HoTen"];
                string mSSV = Request["MSSV"];
                string khoaHoc = Request["Khoa"];
                string lopQuanLy = Request["Lop"];
                string diaChi = Request["DiaChi"];
                string diemTBString = Request["DiemTB"];
                float DiemTB = float.Parse(diemTBString);

                SinhVien newSV = db.SinhViens.Where(o => o.MSSV.Equals(mSSV)).FirstOrDefault();
                newSV.MSSV = mSSV;
                newSV.HoTen = hoTen;
                newSV.Khoa = khoaHoc;
                newSV.Lop = lopQuanLy;
                newSV.DiaChi = diaChi;
                newSV.DiemTB = DiemTB;
                db.SubmitChanges();
                rs.ErrCode = EnumErrCode.Success;
                rs.Message = "Cập nhật thông tin sinh viên thành công";
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Fail;
                rs.ErrDetail = ex.Message;
                rs.Message = "Cập nhật thông tin sinh viên không thành công";
                return JsonConvert.SerializeObject(rs);
            }
            return JsonConvert.SerializeObject(rs);
        }
        public string Delete(string mssv)
        {
            Result_ett<string> rs = new Result_ett<string>();
            try
            {
                SinhVien delObj = db.SinhViens.Where(o => o.MSSV.Equals(mssv)).FirstOrDefault();
                db.SinhViens.DeleteOnSubmit(delObj);
                db.SubmitChanges();
                rs.ErrCode = EnumErrCode.Success;
                rs.Message = "Xóa sinh viên thành công";
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Fail;
                rs.ErrDetail = ex.Message;
                rs.Message = "Xóa sinh viên không thành công";
                return JsonConvert.SerializeObject(rs);
            }
            return JsonConvert.SerializeObject(rs);
        }
    }
}