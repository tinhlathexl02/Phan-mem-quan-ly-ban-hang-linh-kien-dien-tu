﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DALL_BALL;

namespace QuanLyVatLieuXayDung.GUI
{
    public partial class Phieuthurc1 : DevExpress.XtraEditors.XtraUserControl
    {
        DALL_BALL_Phieuthu ctpt = new DALL_BALL_Phieuthu();
        DALL_BALL_KhachHang kh = new DALL_BALL_KhachHang();
        DALL_BALL_NhanVien nv = new DALL_BALL_NhanVien();
        bool add =false, update = false;

        string tinhtrangthanhtoan = "";
        public Phieuthurc1()
        {
            InitializeComponent();
        }

        public void load_phieuthu(object sender, EventArgs e)
        {
            loadhphieuchi();
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            txtMaPhieuThu.Enabled = false;
            loadcombdoituong(Login.macv);
            cboLoaidoituong.SelectedIndex = 0;
        
            bind(true);
      

        }
        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void gridControl1_CursorChanged(object sender, EventArgs e)
        {
        }
        public void loadhphieuchi()
        {
            dsphieuthu.DataSource = ctpt.loadphieuthu();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            bind(false);
            txtSoTienThu.Text = "";
            DateNgayChi.Text = "";
            if (gridView1.RowCount <= 0)
            {
                txtMaPhieuThu.Text = "PT001";

            }
            else
            {
               txtMaPhieuThu.Text=ctpt.getmaphieuthu();
            }
            add = true;
            update = false;

        }

        public void loadcombdoituong(string macv)
        {
            if(macv=="CV002")
            {
                
                cboLoaidoituong.Items.Clear();
                cboLoaidoituong.Items.Add("Khách Hàng");
               
            }
            else
            {
                
                cboLoaidoituong.Items.Clear();
                cboLoaidoituong.Items.Add("Nhân Viên");
            }
            
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            bind(false);
            add = false;
            update = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn Xóa", "Đồng Ý Xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ctpt.xoa1phieuthu(txtMaPhieuThu.Text);
                XtraMessageBox.Show("Thành Công!");
                load_phieuthu(sender, e);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (add)
            {
                if (XtraMessageBox.Show("Bạn có muốn lưu?", "Đồng ý lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mapt = txtMaPhieuThu.Text;
                    string doituong = cboLoaidoituong.Text;
                    string trangthai = "";
                    if(ckTrangThai.Checked)
                    {
                        trangthai = "Đã Thanh Toán";
                    }
                    else
                    {
                        trangthai = "Đang giao dịch";
                    }
                    
                    string manv = Login.manv;
                    int sotienghi = Int32.Parse(txtSoTienThu.Text);
                    DateTime ngayghi = DateTime.Parse(DateNgayChi.Text);
                    ctpt.them1phieuthu(mapt, doituong, trangthai, manv, sotienghi, ngayghi,txtGhiChu.Text);
                    XtraMessageBox.Show("Thành Công!");
                    load_phieuthu(sender, e);
                }
            }
            if (update)
            {
                if (XtraMessageBox.Show("Bạn có muốn sửa?", "Đồng ý sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if(tinhtrangthanhtoan== "Đã Thanh Toán")
                    {
                        XtraMessageBox.Show("Không được sửa phiếu thu đã thanh toán");
                        return;
                    }
                    string mapt = txtMaPhieuThu.Text;
                    string doituong = cboLoaidoituong.Text;
                    string trangthai = "";
                    if (ckTrangThai.Checked)
                    {
                        trangthai = "Đã Thanh Toán";
                    }
                    else
                    {
                        trangthai = "Đang giao dịch";
                    }
                    string manv = Login.manv;
                    int sotienghi = Int32.Parse(txtSoTienThu.Text);
                    DateTime ngayghi = DateTime.Parse(DateNgayChi.Text);
                    ctpt.sua1phieuthu(mapt, doituong, trangthai, manv, sotienghi, ngayghi,txtGhiChu.Text);
                    XtraMessageBox.Show("Thành Công!");
                    load_phieuthu(sender, e);
                }
            }
        }

     

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if(gridView1.RowCount<=0)
            {
                return;
            }
            tinhtrangthanhtoan = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TrangThai").ToString(); ;
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            load_phieuthu(sender, e);
        }

        public void bind(bool t)
        {
            if (t)
            {
                txtMaPhieuThu.DataBindings.Clear();
                txtMaPhieuThu.DataBindings.Add("Text", dsphieuthu.DataSource, "MaPhieuThu");
               
                ckTrangThai.DataBindings.Clear();
                ckTrangThai.DataBindings.Add("Text", dsphieuthu.DataSource, "TrangThai");
             
                txtSoTienThu.DataBindings.Clear();
                txtSoTienThu.DataBindings.Add("Text", dsphieuthu.DataSource, "Sotienchi");
                DateNgayChi.DataBindings.Clear();
                DateNgayChi.DataBindings.Add("Text", dsphieuthu.DataSource, "Ngayghinhan");
                txtGhiChu.DataBindings.Clear();
                txtGhiChu.DataBindings.Add("Text", dsphieuthu.DataSource, "GhiChu");

            }
            else
            {
                txtMaPhieuThu.DataBindings.Clear();
              
                ckTrangThai.DataBindings.Clear();
               
                txtSoTienThu.DataBindings.Clear();
                DateNgayChi.DataBindings.Clear();
                txtGhiChu.DataBindings.Clear();

            }
        }
        public void load_Visble(object sender,EventArgs e)
        
        {
            loadhphieuchi();
        }
    }
}