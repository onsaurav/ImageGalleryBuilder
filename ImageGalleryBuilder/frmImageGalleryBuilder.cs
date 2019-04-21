// Title          :   frmImageGalleryBuilder
// URL            :   ...
// Mail           :   onsaurav@yahoo.com /onsaurav@gmail.com/onsaurav@hotmail.com
// Description    :   The main UI for creating the image galary. 
// Created        :   Saurav Biswas / Jul-25-2011

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace ImageGalleryBuilder
{
    public partial class frmImageGalleryBuilder : Form
    {
        #region Member
        private List<string> oImageKey = new List<string>();        //Array list for storing images key
        private List<string> oImageName = new List<string>();       //Array list for storing images name
        #endregion

        #region Member
        public frmImageGalleryBuilder()
        {
            //Constructor
            InitializeComponent();
        }

        private void ImageGalleryBuilder_Load(object sender, EventArgs e)
        {
            //Array list initialization for storing images name
            oImageName = new List<string>();
            oImageKey = new List<string>();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            //Summary    :   Use to load the images for galary
            //Created    :   Saurav Biswas / Jul-25-2011

            try
            {
                //Openfile dialog using to browse images files
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    //Add image in the imagelist
                    imageList1.Images.Add(new Bitmap(open.FileName));

                    //Add the image name into the arraylist
                    string FNAme = open.SafeFileName.ToLower().Replace(".jpg", "").Replace(".gif", "").Replace(".png", "").Replace(".bmp", "").Replace(".jpeg", "");
                    oImageName.Add(FNAme);
                    oImageKey.Add(open.FileName);

                    //Add the images into the listview
                    ListViewItem LITem = lvwImage.Items.Add(FNAme, imageList1.Images.Count - 1);
                    LITem.Tag = open.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Image Add", MessageBoxButtons.OK);
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            //Summary    :   Use to delete the images from galary
            //Created    :   Saurav Biswas / Jul-25-2011

            try
            {
                if (lvwImage.Items.Count > 0 && lvwImage.SelectedItems.Count > 0)
                {
                    //Remove the image from imagelist
                    imageList1.Images.RemoveAt(lvwImage.SelectedItems[0].Index);

                    //Remove the image name from arraylist
                    oImageName.RemoveAt(lvwImage.SelectedItems[0].Index);
                    oImageKey.RemoveAt(lvwImage.SelectedItems[0].Index);

                    //Load the images
                    LoadImages();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Image Delete", MessageBoxButtons.OK);
            }
        }

        public void LoadImages()
        {
            //Clear the listview
            lvwImage.Items.Clear();
            for (int i = 0; i < oImageName.Count; i++)
            {
                //Relode the listview
                ListViewItem LITem = lvwImage.Items.Add(oImageName[i], i);
                LITem.Tag = oImageKey[i];
            }
        }

        private void tsbUp_Click(object sender, EventArgs e)
        {
            //Summary    :   Use to move the images in previous position in the galary
            //Created    :   Saurav Biswas / Jul-25-2011

            try
            {
                if (lvwImage.Items.Count > 0)
                {
                    if (lvwImage.SelectedItems[0].Index != 0)
                    {
                        int selPosition = lvwImage.SelectedItems[0].Index;

                        //Change the imagelist image position
                        Bitmap revPic = new Bitmap(imageList1.Images[lvwImage.SelectedItems[0].Index]);
                        imageList1.Images[lvwImage.SelectedItems[0].Index] = imageList1.Images[lvwImage.SelectedItems[0].Index - 1];
                        imageList1.Images[lvwImage.SelectedItems[0].Index - 1] = revPic;

                        //Change the arraylist name position
                        string imgName = oImageName[lvwImage.SelectedItems[0].Index];
                        oImageName[lvwImage.SelectedItems[0].Index] = oImageName[lvwImage.SelectedItems[0].Index - 1];
                        oImageName[lvwImage.SelectedItems[0].Index - 1] = imgName;

                        //Change the arraylist key position
                        string imgNameKey = oImageKey[lvwImage.SelectedItems[0].Index];
                        oImageKey[lvwImage.SelectedItems[0].Index] = oImageKey[lvwImage.SelectedItems[0].Index - 1];
                        oImageKey[lvwImage.SelectedItems[0].Index - 1] = imgNameKey;

                        //Load the images
                        LoadImages();

                        lvwImage.Items[selPosition - 1].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Image Up", MessageBoxButtons.OK);
            }
        }

        private void tsbDown_Click(object sender, EventArgs e)
        {
            //Summary    :   Use to move the images in next position in the galary
            //Created    :   Saurav Biswas / Jul-25-2011

            try
            {
                if (lvwImage.Items.Count > 0)
                {
                    if (lvwImage.SelectedItems[0].Index != lvwImage.Items.Count - 1)
                    {
                        int selPosition = lvwImage.SelectedItems[0].Index;

                        //Change the imagelist image position
                        Bitmap revPic = new Bitmap(imageList1.Images[lvwImage.SelectedItems[0].Index]);
                        imageList1.Images[lvwImage.SelectedItems[0].Index] = imageList1.Images[lvwImage.SelectedItems[0].Index + 1];
                        imageList1.Images[lvwImage.SelectedItems[0].Index + 1] = revPic;

                        //Change the arraylist name position
                        string imgName = oImageName[lvwImage.SelectedItems[0].Index];
                        oImageName[lvwImage.SelectedItems[0].Index] = oImageName[lvwImage.SelectedItems[0].Index + 1];
                        oImageName[lvwImage.SelectedItems[0].Index + 1] = imgName;

                        //Change the arraylist key position
                        string imgNamekey = oImageKey[lvwImage.SelectedItems[0].Index];
                        oImageKey[lvwImage.SelectedItems[0].Index] = oImageKey[lvwImage.SelectedItems[0].Index + 1];
                        oImageKey[lvwImage.SelectedItems[0].Index + 1] = imgNamekey;

                        //Load the images
                        LoadImages();

                        lvwImage.Items[selPosition + 1].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Image Up", MessageBoxButtons.OK);
            }
        }

        private void tsbCreate_Click(object sender, EventArgs e)
        {
            if (lvwImage.Items.Count == 0) return;
            panSettings.Visible = false; panThambSettings.Visible = false;
            bool trStart = false; int tdCount = 0;
            FolderBrowserDialog oFBD = new FolderBrowserDialog();
            oFBD.ShowNewFolderButton = false;
            oFBD.RootFolder = System.Environment.SpecialFolder.MyComputer;
            DialogResult result = oFBD.ShowDialog();

            if (result == DialogResult.OK)
            {
                #region Create Folders
                //Create the main directory
                string activeDir = @"" + oFBD.SelectedPath;
                if (activeDir.Substring(activeDir.Length - 1, 1) != "\\") activeDir = activeDir + "\\";
                string newPath = System.IO.Path.Combine(activeDir, GetNewDirectoryName(activeDir + "Imagegalary-001"));
                System.IO.Directory.CreateDirectory(newPath);

                //create the sub directory
                string newImagePathIM = System.IO.Path.Combine(newPath, "images");
                if (Directory.Exists(newImagePathIM) == false) System.IO.Directory.CreateDirectory(newImagePathIM);

                //create the sub directory
                string newImagePathcss = System.IO.Path.Combine(newPath, "css");
                if (Directory.Exists(newImagePathcss) == false) System.IO.Directory.CreateDirectory(newImagePathcss);
                #endregion

                if (cboThambStyle.Text.ToLower() != "Special".ToLower())
                {
                    //create the sub directory
                    string newImagePath = System.IO.Path.Combine(newPath, "images1");
                    if (Directory.Exists(newImagePath) == false) System.IO.Directory.CreateDirectory(newImagePath);

                    //create the sub directory
                    string newImagePathIMthumbnail = System.IO.Path.Combine(newPath, "thumbnail");
                    if (Directory.Exists(newImagePathIMthumbnail) == false) System.IO.Directory.CreateDirectory(newImagePathIMthumbnail);

                    //create the sub directory
                    string newImagePathjs = System.IO.Path.Combine(newPath, "js");
                    if (Directory.Exists(newImagePathjs) == false) System.IO.Directory.CreateDirectory(newImagePathjs);


                    //Create the html galary page
                    StreamWriter file = new StreamWriter(@"" + newPath + "\\index.html");
                    file.WriteLine("<html>");
                    file.WriteLine("<head>");
                    file.WriteLine("<title>");
                    file.WriteLine("Image Galary");
                    file.WriteLine("</title>");
                    if (cboThambStyle.Text.ToLower() != "Special".ToLower())
                    {
                        #region Samecatagory
                        file.WriteLine("<link rel='stylesheet' href='css/ons_galary.css' type='text/css' media='screen' />");
                        file.WriteLine("<script src='js/jquery.min.js' type='text/javascript'></script>");
                        file.WriteLine("<script src='js/ons_galary.js' type='text/javascript'></script>");
                        file.WriteLine("<script src='js/onsdata.js' type='text/javascript'></script>");

                        file.WriteLine("</head>");
                        file.WriteLine("<body>");
                        file.WriteLine("<div align='" + (cboAlignment.Text.Trim() == "" ? "center" : cboAlignment.Text.Trim()) + "'>");
                        file.WriteLine("<table cellspacing='7'>");
                        P.Visible = true; P.Minimum = 0; P.Maximum = lvwImage.Items.Count; P.Value = 0;
                        //save the images

                        int i_Width = 0; int i_Height = 0;
                        i_Width = int.Parse(txtWidth.Text.Trim() == "" ? "100" : txtWidth.Text.Trim());
                        i_Height = int.Parse(txtHeight.Text.Trim() == "" ? "75" : txtHeight.Text.Trim());

                        for (int i = 0; i < oImageKey.Count; i++)
                        {
                            #region Set Extention
                            string ingExt = oImageKey[i].Substring(oImageKey[i].Length - 4, 1);
                            if (ingExt == ".")
                            {
                                ingExt = oImageKey[i].Substring(oImageKey[i].Length - 4, 4);
                            }
                            else
                            {
                                ingExt = oImageKey[i].Substring(oImageKey[i].Length - 5, 1);
                                if (ingExt == ".")
                                {
                                    ingExt = oImageKey[i].Substring(oImageKey[i].Length - 5, 5);
                                }
                                else
                                {
                                    ingExt = ".jpg";
                                }
                            }
                            #endregion
                            #region AddImage In HTML

                            string thamb_Name_str = "";

                            if (i == 0 || i % (int.Parse(txtColumn.Text.Trim() == "" ? "10" : txtColumn.Text.Trim())) == 0)
                            {
                                file.WriteLine("<tr>");
                                trStart = true; tdCount = 0;
                            }
                            file.WriteLine("<td>"); tdCount++;

                            if (File.Exists(oImageKey[i]) == true)
                            {
                                if (File.Exists(newImagePath + "\\" + oImageName[i] + ingExt) == false)
                                {
                                    File.Copy(oImageKey[i], newImagePath + "\\" + oImageName[i] + ingExt, true);
                                    thamb_Name_str = newImagePathIMthumbnail + "\\" + oImageName[i] + ".Jpeg";
                                    file.WriteLine("<a class='onsbox1' href='" + "images1\\" + oImageName[i] + ingExt + "' title='" + oImageName[i] + "'><Img src='" + "thumbnail\\" + oImageName[i] + ".Jpeg' Height='" + i_Height + "' Width='" + i_Width + "' alt ='" + oImageName[i] + "'/></a>");
                                }
                                else
                                {
                                    File.Copy(oImageKey[i], newImagePath + "\\images" + i.ToString() + ingExt, true);
                                    thamb_Name_str = newImagePathIMthumbnail + "\\images" + i.ToString() + ".Jpeg";
                                    file.WriteLine("<a class='onsbox1' href='" + "images1\\images" + i.ToString() + "' title='" + oImageName[i] + "'><Img src='" + "thumbnail\\images" + i.ToString() + ".Jpeg' Height='" + i_Height + "' Width='" + i_Width + "' alt ='" + oImageName[i] + "'/></a>");
                                }
                            }
                            else
                            {
                                imageList1.Images[i].Save(newImagePath + "\\images" + i.ToString() + ingExt);
                                thamb_Name_str = newImagePathIMthumbnail + "\\images" + i.ToString() + ".Jpeg";
                                file.WriteLine("<a class='onsbox1' href='" + "images1\\images" + i.ToString() + "' title='" + oImageName[i] + "'><Img src='" + "thumbnail\\images" + i.ToString() + ".Jpeg' Height='" + i_Height + "' Width='" + i_Width + "' alt ='" + oImageName[i] + "'/></a>");
                            }

                            file.WriteLine("</td>");
                            if ((i == 0 && oImageKey.Count == 0) || ((i + 1) % (int.Parse(txtColumn.Text.Trim() == "" ? "10" : txtColumn.Text.Trim())) == 0) && i != 0)
                            {
                                file.WriteLine("</tr>");
                                trStart = false;
                            }

                            #endregion

                            //Save image for Thumbnail
                            if (ReGenerateThumbnails((Bitmap)imageList1.Images[i], thamb_Name_str) == false) imageList1.Images[i].Save(thamb_Name_str);

                            P.Value = i;
                        }
                        if (trStart == true)
                        {
                            for (int dc = 0; dc < (int.Parse(txtColumn.Text.Trim() == "" ? "10" : txtColumn.Text.Trim()) - tdCount); dc++)
                            {
                                file.WriteLine("<td></td");
                            }
                            file.WriteLine("</tr");
                        }
                        file.WriteLine("</table>");
                        file.WriteLine("</div>");

                        file.WriteLine("</body>");
                        file.WriteLine("</html>");
                        file.Close();

                        #region Storing CSS files and style images
                        string StyleChanged = "AN";
                        if (cboStyle.Text.ToLower() == "Dark_Glass".ToLower())
                        {
                            StyleChanged = "DG";
                        }
                        else if (cboStyle.Text.ToLower() == "facebook".ToLower())
                        {
                            StyleChanged = "FB";
                        }
                        else if (cboStyle.Text.ToLower() == "iPhone".ToLower())
                        {
                            StyleChanged = "IP";
                        }
                        else if (cboStyle.Text.ToLower() == "Mac".ToLower())
                        {
                            StyleChanged = "MC";
                        }
                        else if (cboStyle.Text.ToLower() == "Vista".ToLower())
                        {
                            StyleChanged = "VT";
                        }
                        else
                        {
                            StyleChanged = "AN";
                        }

                        if (StyleChanged == "AN")
                        {
                            File.WriteAllText(@"" + newPath + "\\css\\ons_galary.css", Properties.Resources.in_css);

                            Properties.Resources.in_back_cb.Save(@"" + newPath + "\\images\\back_cb.png");
                            Properties.Resources.in_back_corvers.Save(@"" + newPath + "\\images\\back_corvers.png");
                            Properties.Resources.in_back_ct.Save(@"" + newPath + "\\images\\back_ct.png");
                            Properties.Resources.in_back_info.Save(@"" + newPath + "\\images\\back_info.png");
                            Properties.Resources.in_back_lb.Save(@"" + newPath + "\\images\\back_lb.png");
                            Properties.Resources.in_back_lc.Save(@"" + newPath + "\\images\\back_lc.png");
                            Properties.Resources.in_back_lr.Save(@"" + newPath + "\\images\\back_lr.png");
                            Properties.Resources.in_back_lt.Save(@"" + newPath + "\\images\\back_lt.png");
                            Properties.Resources.in_back_rb.Save(@"" + newPath + "\\images\\back_rb.png");
                            Properties.Resources.in_back_rc.Save(@"" + newPath + "\\images\\back_rc.png");
                            Properties.Resources.in_back_rt.Save(@"" + newPath + "\\images\\back_rt.png");
                            Properties.Resources.in_back_tb.Save(@"" + newPath + "\\images\\back_tb.png");
                            Properties.Resources.in_cancel48color.Save(@"" + newPath + "\\images\\cancel48color.png");
                            Properties.Resources.in_cancel48grey.Save(@"" + newPath + "\\images\\cancel48grey.png");
                            Properties.Resources.in_close.Save(@"" + newPath + "\\images\\close.png");
                            Properties.Resources.in_loading.Save(@"" + newPath + "\\images\\loading.gif");
                            Properties.Resources.in_navigation.Save(@"" + newPath + "\\images\\navigation.png");
                            Properties.Resources.in_next.Save(@"" + newPath + "\\images\\next.png");
                            Properties.Resources.in_next32_color.Save(@"" + newPath + "\\images\\next32-color.png");
                            Properties.Resources.in_next32_grey.Save(@"" + newPath + "\\images\\next32-grey.png");
                            Properties.Resources.in_pause32color.Save(@"" + newPath + "\\images\\pause32color.png");
                            Properties.Resources.in_pause32grey.Save(@"" + newPath + "\\images\\pause32grey.png");
                            Properties.Resources.in_prev_32color.Save(@"" + newPath + "\\images\\prev-32color.png");
                            Properties.Resources.in_prev_32grey.Save(@"" + newPath + "\\images\\prev-32grey.png");
                            Properties.Resources.in_prev.Save(@"" + newPath + "\\images\\prev.png");
                            Properties.Resources.in_start32color.Save(@"" + newPath + "\\images\\start32color.png");
                            Properties.Resources.in_start32grey.Save(@"" + newPath + "\\images\\start32grey.png");
                        }
                        else if (StyleChanged == "IP")
                        {
                            File.WriteAllText(@"" + newPath + "\\css\\ons_galary.css", Properties.Resources.ip_css);

                            Properties.Resources.ip_back_cb.Save(@"" + newPath + "\\images\\back_cb.png");
                            Properties.Resources.ip_back_corvers.Save(@"" + newPath + "\\images\\back_corvers.png");
                            Properties.Resources.ip_back_ct.Save(@"" + newPath + "\\images\\back_ct.png");
                            Properties.Resources.ip_back_lb.Save(@"" + newPath + "\\images\\back_lb.png");
                            Properties.Resources.ip_back_lc.Save(@"" + newPath + "\\images\\back_lc.png");
                            Properties.Resources.ip_back_lr.Save(@"" + newPath + "\\images\\back_lr.png");
                            Properties.Resources.ip_back_lt.Save(@"" + newPath + "\\images\\back_lt.png");
                            Properties.Resources.ip_back_rb.Save(@"" + newPath + "\\images\\back_rb.png");
                            Properties.Resources.ip_back_rc.Save(@"" + newPath + "\\images\\back_rc.png");
                            Properties.Resources.ip_back_rt.Save(@"" + newPath + "\\images\\back_rt.png");
                            Properties.Resources.ip_back_tb.Save(@"" + newPath + "\\images\\back_tb.png");
                            Properties.Resources.ip_bk.Save(@"" + newPath + "\\images\\bk.png");
                            Properties.Resources.ip_loading.Save(@"" + newPath + "\\images\\loading.gif");
                            Properties.Resources.ip_navigation.Save(@"" + newPath + "\\images\\navigation.gif");
                            Properties.Resources.ip_navigation1.Save(@"" + newPath + "\\images\\navigation.png");
                            Properties.Resources.ip_next.Save(@"" + newPath + "\\images\\next.gif");
                            Properties.Resources.ip_next1.Save(@"" + newPath + "\\images\\next.png");
                            Properties.Resources.ip_prev.Save(@"" + newPath + "\\images\\prev.gif");
                            Properties.Resources.ip_prev1.Save(@"" + newPath + "\\images\\prev.png");
                        }
                        else if (StyleChanged == "FB")
                        {
                            File.WriteAllText(@"" + newPath + "\\css\\ons_galary.css", Properties.Resources.fb_css);

                            Properties.Resources.fb_borderBottomCenter.Save(@"" + newPath + "\\images\\borderBottomCenter.png");
                            Properties.Resources.fb_borderBottomLeft.Save(@"" + newPath + "\\images\\borderBottomLeft.png");
                            Properties.Resources.fb_borderBottomRight.Save(@"" + newPath + "\\images\\borderBottomRight.png");
                            Properties.Resources.fb_borderMiddleLeft.Save(@"" + newPath + "\\images\\borderMiddleLeft.png");
                            Properties.Resources.fb_borderMiddleRight.Save(@"" + newPath + "\\images\\borderMiddleRight.png");
                            Properties.Resources.fb_borderTopCenter.Save(@"" + newPath + "\\images\\borderTopCenter.png");
                            Properties.Resources.fb_borderTopLeft.Save(@"" + newPath + "\\images\\borderTopLeft.png");
                            Properties.Resources.fb_borderTopRight.Save(@"" + newPath + "\\images\\borderTopRight.png");
                            Properties.Resources.fb_closelabel.Save(@"" + newPath + "\\images\\closelabel.gif");
                            Properties.Resources.fb_loading.Save(@"" + newPath + "\\images\\loading.gif");
                            Properties.Resources.fb_next.Save(@"" + newPath + "\\images\\next.png");
                            Properties.Resources.fb_nextlabel.Save(@"" + newPath + "\\images\\nextlabel.gif");
                            Properties.Resources.fb_next_ie6.Save(@"" + newPath + "\\images\\next_ie6.gif");
                            Properties.Resources.ip_navigation.Save(@"" + newPath + "\\images\\navigation.gif");
                            Properties.Resources.ip_navigation1.Save(@"" + newPath + "\\images\\navigation.png");
                            Properties.Resources.fb_pause.Save(@"" + newPath + "\\images\\pause.gif");
                            Properties.Resources.fb_prev.Save(@"" + newPath + "\\images\\prev.png");
                            Properties.Resources.fb_prevlabel.Save(@"" + newPath + "\\images\\prevlabel.gif");
                            Properties.Resources.fb_prev_ie6.Save(@"" + newPath + "\\images\\prev_ie6.gif");
                            Properties.Resources.fb_start.Save(@"" + newPath + "\\images\\start.gif");
                        }
                        else if (StyleChanged == "MC")
                        {
                            File.WriteAllText(@"" + newPath + "\\css\\ons_galary.css", Properties.Resources.mc_css);

                            Properties.Resources.mc_borderBottomCenter.Save(@"" + newPath + "\\images\\borderBottomCenter.png");
                            Properties.Resources.mc_borderBottomLeft.Save(@"" + newPath + "\\images\\borderBottomLeft.png");
                            Properties.Resources.mc_borderBottomRight.Save(@"" + newPath + "\\images\\borderBottomRight.png");
                            Properties.Resources.mc_borderMiddleLeft.Save(@"" + newPath + "\\images\\borderMiddleLeft.png");
                            Properties.Resources.mc_borderMiddleRight.Save(@"" + newPath + "\\images\\borderMiddleRight.png");
                            Properties.Resources.mc_borderTopCenter.Save(@"" + newPath + "\\images\\borderTopCenter.png");
                            Properties.Resources.mc_borderTopLeft.Save(@"" + newPath + "\\images\\borderTopLeft.png");
                            Properties.Resources.mc_borderTopRight.Save(@"" + newPath + "\\images\\borderTopRight.png");
                            Properties.Resources.mc_close.Save(@"" + newPath + "\\images\\close.gif");
                            Properties.Resources.mc_closelabel.Save(@"" + newPath + "\\images\\closelabel.gif");
                            Properties.Resources.mc_loading.Save(@"" + newPath + "\\images\\loading.gif");
                            Properties.Resources.mc_next.Save(@"" + newPath + "\\images\\next.png");
                            Properties.Resources.mc_nextlabel.Save(@"" + newPath + "\\images\\nextlabel.gif");
                            Properties.Resources.mc_next_ie6.Save(@"" + newPath + "\\images\\next_ie6.gif");
                            Properties.Resources.mc_pause.Save(@"" + newPath + "\\images\\pause.gif");
                            Properties.Resources.mc_prev.Save(@"" + newPath + "\\images\\prev.png");
                            Properties.Resources.mc_prevlabel.Save(@"" + newPath + "\\images\\prevlabel.gif");
                            Properties.Resources.mc_prev_ie6.Save(@"" + newPath + "\\images\\prev_ie6.gif");
                            Properties.Resources.mc_start.Save(@"" + newPath + "\\images\\start.gif");
                        }
                        else if (StyleChanged == "VT")
                        {
                            File.WriteAllText(@"" + newPath + "\\css\\ons_galary.css", Properties.Resources.vt_css);

                            Properties.Resources.vt_back.Save(@"" + newPath + "\\images\\back.png");
                            Properties.Resources.vt_borderBottomCenter.Save(@"" + newPath + "\\images\\borderBottomCenter.png");
                            Properties.Resources.vt_borderBottomLeft.Save(@"" + newPath + "\\images\\borderBottomLeft.png");
                            Properties.Resources.vt_borderBottomRight.Save(@"" + newPath + "\\images\\borderBottomRight.png");
                            Properties.Resources.vt_borderMiddleLeft.Save(@"" + newPath + "\\images\\borderMiddleLeft.png");
                            Properties.Resources.vt_borderMiddleRight.Save(@"" + newPath + "\\images\\borderMiddleRight.png");
                            Properties.Resources.vt_borderTopCenter.Save(@"" + newPath + "\\images\\borderTopCenter.png");
                            Properties.Resources.vt_borderTopLeft.Save(@"" + newPath + "\\images\\borderTopLeft.png");
                            Properties.Resources.vt_borderTopRight.Save(@"" + newPath + "\\images\\borderTopRight.png");
                            Properties.Resources.vt_close.Save(@"" + newPath + "\\images\\close.png");
                            Properties.Resources.vt_conposit.Save(@"" + newPath + "\\images\\conposit.png");
                            Properties.Resources.vt_loading.Save(@"" + newPath + "\\images\\loading.gif");
                            Properties.Resources.vt_next.Save(@"" + newPath + "\\images\\next.png");
                            Properties.Resources.vt_next_ie6.Save(@"" + newPath + "\\images\\next_ie6.gif");
                            Properties.Resources.vt_prev.Save(@"" + newPath + "\\images\\prev.png");
                            Properties.Resources.vt_prev_ie6.Save(@"" + newPath + "\\images\\prev_ie6.gif");
                        }
                        else if (StyleChanged == "DG")
                        {
                            File.WriteAllText(@"" + newPath + "\\css\\ons_galary.css", Properties.Resources.dg_css);

                            Properties.Resources.dg_start32color.Save(@"" + newPath + "\\images\\start32grey.png");
                            Properties.Resources.dg_back_corvers.Save(@"" + newPath + "\\images\\back_corvers.png");
                            Properties.Resources.dg_back_ct.Save(@"" + newPath + "\\images\\back_ct.png");
                            Properties.Resources.dg_back_info.Save(@"" + newPath + "\\images\\back_info.png");
                            Properties.Resources.dg_back_lb.Save(@"" + newPath + "\\images\\back_lb.png");
                            Properties.Resources.dg_back_lc.Save(@"" + newPath + "\\images\\back_lc.png");
                            Properties.Resources.dg_back_lr.Save(@"" + newPath + "\\images\\back_lr.png");
                            Properties.Resources.dg_back_lt.Save(@"" + newPath + "\\images\\back_lt.png");
                            Properties.Resources.dg_back_rb.Save(@"" + newPath + "\\images\\back_rb.png");
                            Properties.Resources.dg_back_rc.Save(@"" + newPath + "\\images\\back_rc.png");
                            Properties.Resources.dg_back_rt.Save(@"" + newPath + "\\images\\back_rt.png");
                            Properties.Resources.dg_back_tb.Save(@"" + newPath + "\\images\\back_tb.png");
                            Properties.Resources.dg_cancel48color.Save(@"" + newPath + "\\images\\cancel48color.png");
                            Properties.Resources.dg_cancel48grey.Save(@"" + newPath + "\\images\\cancel48grey.png");
                            Properties.Resources.dg_close.Save(@"" + newPath + "\\images\\close.png");
                            Properties.Resources.dg_dg_back_cb.Save(@"" + newPath + "\\images\\back_cb.png");
                            Properties.Resources.dg_loading.Save(@"" + newPath + "\\images\\loading.gif");
                            Properties.Resources.dg_navigation.Save(@"" + newPath + "\\images\\navigation.png");
                            Properties.Resources.dg_next.Save(@"" + newPath + "\\images\\next.gif");
                            Properties.Resources.dg_next.Save(@"" + newPath + "\\images\\next.png");
                            Properties.Resources.dg_next32_color.Save(@"" + newPath + "\\images\\next32-color.png");
                            Properties.Resources.dg_next32_grey.Save(@"" + newPath + "\\images\\next32-grey.png");
                            Properties.Resources.dg_pause32color.Save(@"" + newPath + "\\images\\pause32color.png");
                            Properties.Resources.dg_pause32grey.Save(@"" + newPath + "\\images\\pause32grey.png");
                            Properties.Resources.dg_prev_32color.Save(@"" + newPath + "\\images\\prev-32color.png");
                            Properties.Resources.dg_prev_32grey.Save(@"" + newPath + "\\images\\prev-32grey.png");
                            Properties.Resources.dg_prev.Save(@"" + newPath + "\\images\\prev.gif");
                            Properties.Resources.dg_prev1.Save(@"" + newPath + "\\images\\prev.png");
                            Properties.Resources.dg_start32color.Save(@"" + newPath + "\\images\\start32color.png");
                        }
                        #endregion
                        #region saveing JQuerry Files
                        File.WriteAllText(@"" + newPath + "\\js\\jquery.min.js", Properties.Resources.js_min);
                        File.WriteAllText(@"" + newPath + "\\js\\ons_galary.js", Properties.Resources.ons_galary);
                        File.WriteAllText(@"" + newPath + "\\js\\onsdata.js", Properties.Resources.onsdata);
                        #endregion
                        #endregion
                    }

                }
                else
                {
                    #region HTML
                    //Create the html galary page
                    StreamWriter file = new StreamWriter(@"" + newPath + "\\index.html");
                    file.WriteLine("<html>");
                    file.WriteLine("<head>");
                    file.WriteLine("<title>");
                    file.WriteLine("Image Galary");
                    file.WriteLine("</title>");
                    file.WriteLine("<link rel='stylesheet' href='css/ons_style.css' type='text/css' media='screen' />");
                    file.WriteLine("</head>");
                    file.WriteLine("<body>");

                    file.WriteLine("<div id='gallery'>");
                    file.WriteLine("<div id='slides'>");

                    int steps = 0; int c = 0;
                    double icnt = 0; icnt = (lvwImage.Items.Count - int.Parse(txtColumn.Text.Trim() == "" ? "5" : txtColumn.Text.Trim())) + 3;
                    for (int ic = 0; ic < icnt; ic++)
                    {
                        file.WriteLine("<div id='slide" + ic + "'>");
                        file.WriteLine("<ul>");
                        for (int cl = 1; cl <= int.Parse(txtColumn.Text.Trim() == "" ? "5" : txtColumn.Text.Trim()); cl++)
                        {
                            if (cl == 1)
                            {
                                if (steps == 0)
                                {
                                    file.WriteLine("<li class='blank'><img class='previous' src='./images/upx.png' alt='' title=''/></li>");
                                }
                                else
                                {
                                    file.WriteLine("<li class='blank'><a href='#slide" + (ic - 1) + "'><img class='previous' src='./images/upx.png' alt='' title=''/></a></li>");
                                }
                            }
                            else if (cl == int.Parse(txtColumn.Text.Trim() == "" ? "5" : txtColumn.Text.Trim()))
                            {
                                if ((steps / int.Parse(txtColumn.Text.Trim() == "" ? "5" : txtColumn.Text.Trim())) == icnt)
                                {
                                    file.WriteLine("<li class='blank'><img class='next' src='./images/downx.png' alt='' title=''/></li>");
                                }
                                else
                                {
                                    file.WriteLine("<li class='blank'><a href='#slide" + (ic + 1) + "'><img class='next' src='./images/downx.png' alt='' title=''/></a></li>");
                                }
                            }
                            else
                            {

                                if (((cl - 2) + c) > oImageKey.Count)
                                {
                                    string ex1 = getExtention(oImageKey[(cl - 2) - c]);
                                    file.WriteLine("<li><a href='#pic" + ((cl - 1) - c) + "'><img src='./images/" + oImageName[(cl - 1) - c] + ex1 + "' alt='Winter branches' title=''/></a></li>");
                                }
                                else
                                {
                                    string ex2 = getExtention(oImageKey[(cl - 2) + c]);
                                    file.WriteLine("<li><a href='#pic" + ((cl - 2) + c) + "'><img src='./images/" + oImageName[(cl - 2) + c] + ex2 + "' alt='Winter branches' title=''/></a></li>");
                                }
                            }
                        }
                        c++;
                        steps = steps + int.Parse(txtColumn.Text.Trim() == "" ? "5" : txtColumn.Text.Trim());
                        file.WriteLine("</ul>");
                        file.WriteLine("</div>");
                    }
                    file.WriteLine("</div>");

                    file.WriteLine("<div id='fullsize'>");
                    for (int fji = 0; fji < oImageName.Count; fji++)
                    {
                        string ex = getExtention(oImageKey[fji]);
                        file.WriteLine("<div id='pic" + fji + "'><img src='./images/" + oImageName[fji] + ex + "' alt='" + oImageName[fji] + "'/></div>");
                    }
                    file.WriteLine("</div>");
                    file.WriteLine("</div>");

                    file.WriteLine("</body>");
                    file.WriteLine("</html>");
                    file.Close();
                    #endregion

                    #region CSS
                    //Create the css
                    int fx = 50; int fy = 50;
                    if (double.Parse(txtHeight.Text == "" ? "50" : txtHeight.Text) < 50) fx = int.Parse(txtHeight.Text == "" ? "50" : txtHeight.Text);
                    if (double.Parse(txtWidth.Text == "" ? "50" : txtWidth.Text) < 50) fy = int.Parse(txtWidth.Text == "" ? "50" : txtWidth.Text);
                    StreamWriter file_css = new StreamWriter(@"" + newImagePathcss + "\\ons_style.css");

                    double MW = 0; double MH = 0; double W = 0; double H = 0;
                    W = double.Parse(txtWidth.Text == "" ? "92" : txtWidth.Text);
                    H = double.Parse(txtHeight.Text == "" ? "62" : txtHeight.Text);
                    MW = double.Parse(txtMiWidth.Text == "" ? "750" : txtMiWidth.Text);
                    MH = double.Parse(txtMIHeight.Text == "" ? "550" : txtMIHeight.Text);

                    if (cboAlignment.Text.ToLower() == "Right".ToLower())
                    {
                        file_css.WriteLine("#gallery {width: " + MW + "px; height: " + MH + "px; margin: 0 auto; position: relative; font-family: verdana,arial,sans-serif; background: #fff;}");
                        file_css.WriteLine("#gallery #slides div {width: " + W + "px; height: " + MH + "px; position: relative; padding-top: 10px;}");
                        file_css.WriteLine("#gallery #slides div ul {padding: 0; margin: 0; width: " + W + "px; height: " + MH + "px; list-style:none; text-align:center; }");
                        file_css.WriteLine("#gallery #slides div ul li {float: left; padding: 1px 0; width: " + W + "px; height: " + H + "px; }");
                        file_css.WriteLine("#gallery #slides div ul li.blank {float: left; padding: 1px 0; width: " + W + "px; height: "+ fx +"px; }");
                        file_css.WriteLine("#gallery #slides div ul li a img {display: block; width: " + W + "px; height: " + H + "px; border: 0; margin: 0 auto;}");
                        file_css.WriteLine("#gallery #slides div ul li a img.previous {background: url(images/upx.png) no-repeat center center; width: " + fy + "px; height: " + fx + "px; }");
                        file_css.WriteLine("#gallery #slides div ul li a img.next {background: url(images/downx.png) no-repeat center center; width: " + fy + "px; height: " + fx + "px; }");
                        
                        file_css.WriteLine("#slides {top:0; position: absolute; right: 10px; height: " + MH + "px; width: " + W + "px; overflow: hidden; text-align: center; z-index:500;  left:" + ((MW - W) + 25) + "px;}");
                        file_css.WriteLine("#gallery #fullsize {position: absolute; left: 0; top: 0; height: " + MH + "px; width: " + (MW - W) + "px; overflow: hidden; text-align: center; z-index: 200;}");

                        file_css.WriteLine("#gallery #fullsize div {width: " + ((MW - W) - 75) + "px; height: " + (MH + 100) + "px; padding-top: 35px; position: relative; }");
                        file_css.WriteLine("#gallery #fullsize div img {clear: both; display: block; margin: 0 auto; border: 1px solid #eee; width: " + ((MW - W)-25) + "px; height: " + ((MH - H)-25) + "px; position: relative; background: #fff; padding: 10px;}");
                    }
                    else if (cboAlignment.Text.ToLower() == "Left".ToLower())
                    {
                        file_css.WriteLine("#gallery {width: " + MW + "px; height: " + MH + "px; margin: 0 auto; position: relative; font-family: verdana,arial,sans-serif; background: #fff;}");
                        file_css.WriteLine("#gallery #slides div {width: " + W + "px; height: " + MH + "px; position: relative; padding-top: 10px;}");
                        file_css.WriteLine("#gallery #slides div ul {padding: 0; margin: 0; width: " + W + "px; height: " + MH + "px; list-style:none; text-align:center; }");
                        file_css.WriteLine("#gallery #slides div ul li {float: left; padding: 1px 0; width: " + W + "px; height: " + H + "px; }");
                        file_css.WriteLine("#gallery #slides div ul li.blank {float: left; padding: 1px 0; width: " + W + "px; height: " + fx + "px; }");
                        file_css.WriteLine("#gallery #slides div ul li a img {display: block; width: " + W + "px; height: " + H + "px; border: 0; margin: 0 auto;}");
                        file_css.WriteLine("#gallery #slides div ul li a img.previous {background: url(images/upx.png) no-repeat center center; width: " + fy + "px; height: " + fx + "px; }");
                        file_css.WriteLine("#gallery #slides div ul li a img.next {background: url(images/downx.png) no-repeat center center; width: " + fy + "px; height: " + fx + "px; }");
                        
                        file_css.WriteLine("#slides {top:0; position: absolute; right: 10px; height: " + MH + "px; width: " + W + "px; overflow: hidden; text-align: center; z-index:500;  left:0}");
                        file_css.WriteLine("#gallery #fullsize {position: absolute; left: " + (W + 25) + "; top: 0; height: " + MH + "px; width: " + (MW - W) + "px; overflow: hidden; text-align: center; z-index: 200;}");

                        file_css.WriteLine("#gallery #fullsize div {width: " + ((MW - W) - 75) + "px; height: " + (MH + 100) + "px; padding-top: 35px; position: relative; }");
                        file_css.WriteLine("#gallery #fullsize div img {clear: both; display: block; margin: 0 auto; border: 1px solid #eee; width: " + ((MW - W)-25) + "px; height: " + ((MH - H)-25) + "px; position: relative; background: #fff; padding: 10px;}");
                    }
                    else if (cboAlignment.Text.ToLower() == "Top".ToLower())
                    {
                        file_css.WriteLine("#gallery {width: "+ MW +"px; height: "+ MH +"px; margin: 0 auto; position: relative; font-family: verdana,arial,sans-serif; background: #fff;text-align:center; }");
                        file_css.WriteLine("#gallery #slides div {width: "+ MW +"px; height: 75px; position: relative;text-align:center; }");
                        file_css.WriteLine("#gallery #slides div ul {padding: 0; margin: 0; width: "+ (MW-W) +"px; height: "+ W +"px; list-style-type:none; text-align:center; left:100px;padding-left:"+ Math.Round(W/5) +"px;}");
                        file_css.WriteLine("#gallery #slides div ul li {display: inline; float: left; width: "+ W +"px; height: "+ H +"px; }");
                        file_css.WriteLine("#gallery #slides div ul li.blank {width: 50px; height: 50px;}");
                        file_css.WriteLine("#gallery #slides div ul li a img {display: block; width: "+ (W-3) +"px; height: "+ H +"px; border: 0; margin: 0 auto;}");
                        file_css.WriteLine("#gallery #slides div ul li a img.previous {background: url(images/upx.png) no-repeat center center; width: 50px; height: 50px; }");
                        file_css.WriteLine("#gallery #slides div ul li a img.next {background: url(images/downx.png) no-repeat center center; width: 50px; height: 50px; }");
                        file_css.WriteLine("#slides {top:0; position: absolute; height: "+ H +"px; width: "+ MW +"px; overflow: hidden; text-align: center; z-index:500;  left:0;}");
                        file_css.WriteLine("#gallery #fullsize {position: absolute; left: 0px; top: "+ (H+10) +"px; height: "+ ((MH-H)-10) +"px; width: "+ MW +"px; overflow: hidden; text-align: center; z-index: 200;}");
                        file_css.WriteLine("#gallery #fullsize div {width: " + (MW - W) + "px; height: " + (MH - H) + "px; position: relative; text-align:center;left:0px; padding-top:10px;}");
                        file_css.WriteLine("#gallery #fullsize div img {clear: both; display: block; margin: 0 auto; border: 1px solid #eee; width: "+ ((MW-W)-25) +"px; height: "+ ((MH-H)-25) +"px; position: relative; background: #fff;left:0px;}");
                    }
                    else if (cboAlignment.Text.ToLower() == "Buttom".ToLower())
                    {
                        file_css.WriteLine("#gallery {width: " + MW + "px; height: " + MH + "px; margin: 0 auto; position: relative; font-family: verdana,arial,sans-serif; background: #fff;text-align:center; }");
                        file_css.WriteLine("#gallery #slides div {width: " + MW + "px; height: 75px; position: relative;text-align:center; }");
                        file_css.WriteLine("#gallery #slides div ul {padding: 0; margin: 0; width: " + (MW - W) + "px; height: " + W + "px; list-style-type:none; text-align:center; left:100px;padding-left:" + Math.Round(W / 5) + "px;}");
                        file_css.WriteLine("#gallery #slides div ul li {display: inline; float: left; width: " + W + "px; height: " + H + "px; }");
                        file_css.WriteLine("#gallery #slides div ul li.blank {width: 50px; height: 50px;}");
                        file_css.WriteLine("#gallery #slides div ul li a img {display: block; width: " + (W - 3) + "px; height: " + H + "px; border: 0; margin: 0 auto;}");
                        file_css.WriteLine("#gallery #slides div ul li a img.previous {background: url(images/upx.png) no-repeat center center; width: 50px; height: 50px; }");
                        file_css.WriteLine("#gallery #slides div ul li a img.next {background: url(images/downx.png) no-repeat center center; width: 50px; height: 50px; }");
                        file_css.WriteLine("#slides {top:" + ((MH - H) + 10) + "px; position: absolute; height: " + H + "px; width: " + MW + "px; overflow: hidden; text-align: center; z-index:500;  left:0;}");
                        file_css.WriteLine("#gallery #fullsize {position: absolute; left: 0px; top: 0; height: " + ((MH - H) - 10) + "px; width: " + MW + "px; overflow: hidden; text-align: center; z-index: 200;}");
                        file_css.WriteLine("#gallery #fullsize div {width: " + (MW - W) + "px; height: " + (MH - H) + "px; position: relative; text-align:center;left:0px; padding-top:10px;}");
                        file_css.WriteLine("#gallery #fullsize div img {clear: both; display: block; margin: 0 auto; border: 1px solid #eee; width: " + ((MW - W) - 25) + "px; height: " + ((MH - H) - 25) + "px; position: relative; background: #fff;left:0px;}");
                    }
                    file_css.Close();
                    #endregion

                    #region CopyImages
                    for (int i = 0; i < oImageKey.Count; i++)
                    {
                        string ingExt11 = getExtention(oImageKey[i]);

                        if (File.Exists(oImageKey[i]) == true)
                        {
                            if (File.Exists(newImagePathIM + "\\" + oImageName[i] + ingExt11) == false)
                            {
                                File.Copy(oImageKey[i], newImagePathIM + "\\" + oImageName[i] + ingExt11, true);
                            }
                            else
                            {
                                File.Copy(oImageKey[i], newImagePathIM + "\\" + i.ToString() + ingExt11, true);
                            }
                        }
                        else
                        {
                            imageList1.Images[i].Save(newImagePathIM + "\\" + i.ToString() + ingExt11);
                        }
                    }
                    Properties.Resources.upx.Save(@"" + newImagePathIM + "\\upx.png");
                    Properties.Resources.downx.Save(@"" + newImagePathIM + "\\downx.png");
                    #endregion
                }

                MessageBox.Show("Successfuly create the html file.");
                P.Visible = false;
            }
        }

        public string GetNewDirectoryName(string path)
        {
            int fileno = 0;
            if (Directory.Exists(path))
            {
                fileno = int.Parse(path.Substring(path.Length - 3, 3));
                fileno++;
                path = path.Substring(0, path.Length - 3);
                path = path + fileno.ToString("000");
                path = GetNewDirectoryName(path);
            }
            return path;
        }

        private void tsbAddAll_Click(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();
                //browse the folder for images
                FolderBrowserDialog oFBD = new FolderBrowserDialog();
                oFBD.ShowNewFolderButton = false;
                oFBD.RootFolder = System.Environment.SpecialFolder.MyComputer;
                DialogResult result = oFBD.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //get all files from the selected folder
                    System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(oFBD.SelectedPath);
                    System.IO.FileInfo[] files = dirInfo.GetFiles("*.*");
                    if (files != null)
                    {
                        P.Visible = true; P.Minimum = 0; P.Maximum = files.Length; P.Value = 0;
                        foreach (System.IO.FileInfo file in files)
                        {
                            Application.DoEvents();
                            try
                            {
                                //Checking the file extention
                                string ext = file.Name.Substring(file.Name.Length - 4, 4).ToLower();
                                string ext1 = file.Name.Substring(file.Name.Length - 5, 5).ToLower();
                                if (ext == ".jpg" || ext == ".gif" || ext == ".png" || ext == ".bmp" || ext1 == ".jpeg")
                                {
                                    string FPath = oFBD.SelectedPath + "\\" + file.Name;

                                    //Add image in the imagelist
                                    imageList1.Images.Add(new Bitmap(FPath));

                                    //Add the image name into the arraylist
                                    string FName = file.Name.ToLower().Replace(".jpg", "").Replace(".gif", "").Replace(".png", "").Replace(".bmp", "").Replace(".jpeg", "");
                                    oImageName.Add(FName);
                                    oImageKey.Add(FPath);

                                    //Add the images into the listview
                                    ListViewItem LITem = lvwImage.Items.Add(FName, imageList1.Images.Count - 1);
                                    LITem.Tag = FPath;
                                }
                            }
                            catch { }
                            P.Value = P.Value + 1;
                        }
                        P.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Image All", MessageBoxButtons.OK);
            }
            P.Visible = false;
        }

        private void frmImageGalleryBuilder_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Terminate the application
            Application.Exit();
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            //Clear All
            lvwImage.Items.Clear();
            imageList1.Images.Clear();
            oImageName.Clear();
            oImageKey.Clear();
        }

        private void tsbSettings_Click(object sender, EventArgs e)
        {
            panSettings.Visible = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            panSettings.Visible = false;
        }

        private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStyle.Text.ToLower() == "Androied".ToLower())
            {
                picTumb.Image = Properties.Resources.an_thumbnail;
            }
            else if (cboStyle.Text.ToLower() == "Dark_Glass".ToLower())
            {
                picTumb.Image = Properties.Resources.dg_thumbnail;
            }
            else if (cboStyle.Text.ToLower() == "facebook".ToLower())
            {
                picTumb.Image = Properties.Resources.fb_thumbnail;
            }
            else if (cboStyle.Text.ToLower() == "iPhone".ToLower())
            {
                picTumb.Image = Properties.Resources.ip_thumbnail;
            }
            else if (cboStyle.Text.ToLower() == "Mac".ToLower())
            {
                picTumb.Image = Properties.Resources.mc_thumbnail;
            }
            else if (cboStyle.Text.ToLower() == "Vista".ToLower())
            {
                picTumb.Image = Properties.Resources.vt_thumbnail;
            }
        }
        
        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
            saveFileDialog1.Filter = "Your extension here (*.ibp)|*.ibp";
            saveFileDialog1.FilterIndex = 1;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter file = new StreamWriter(@"" + saveFileDialog1.FileName);
                    file.WriteLine(cboStyle.Text + "<ONS>" + cboThambStyle.Text + "<ONS>" + txtWidth.Text + "<ONS>" + txtHeight.Text + "<ONS>" + txtColumn.Text);
                    file.Close();

                    string NewfolderName = saveFileDialog1.FileName;
                    string[] sep = { "\\" };
                    string[] words = NewfolderName.Split(sep, StringSplitOptions.None);

                    NewfolderName = "";
                    for (int i = 0; i < words.Length - 1; i++)
                    {
                        if (NewfolderName != "") NewfolderName = NewfolderName + "\\";
                        NewfolderName = NewfolderName + words[i];
                    }

                    string newImagePath = System.IO.Path.Combine(NewfolderName, "images");
                    if (Directory.Exists(newImagePath) == false) System.IO.Directory.CreateDirectory(newImagePath);

                    for (int i = 0; i < oImageKey.Count; i++)
                    {
                        string ingExt = oImageKey[i].Substring(oImageKey[i].Length - 4, 1);
                        if (ingExt == ".")
                        {
                            ingExt = oImageKey[i].Substring(oImageKey[i].Length - 4, 4);
                        }
                        else
                        {
                            ingExt = oImageKey[i].Substring(oImageKey[i].Length - 5, 1);
                            if (ingExt == ".")
                            {
                                ingExt = oImageKey[i].Substring(oImageKey[i].Length - 5, 5);
                            }
                            else
                            {
                                ingExt = ".jpg";
                            }
                        }

                        if (File.Exists(oImageKey[i]) == true)
                        {
                            if (File.Exists(newImagePath + "\\" + oImageName[i] + ingExt) == false)
                            {
                                File.Copy(oImageKey[i], newImagePath + "\\" + oImageName[i] + ingExt, true);
                            }
                            else
                            {
                                File.Copy(oImageKey[i], newImagePath + "\\images" + i.ToString() + ingExt, true);
                            }
                        }
                        else
                        {
                            imageList1.Images[i].Save(newImagePath + "\\images" + i.ToString() + ingExt);
                        }
                    }
                    MessageBox.Show("File saved successfully.", "Save", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Save", MessageBoxButtons.OK);
                }
            }
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.ibp;)|*.ibp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                string text = System.IO.File.ReadAllText(@"" + open.FileName);
                try
                {
                    string[] sep111 = { "<ONS>" };
                    string[] fieSettings = text.Replace("\r", "").Replace("\n", "").Trim().Split(sep111, StringSplitOptions.None);

                    cboStyle.Text = "";
                    cboThambStyle.Text = "";
                    txtWidth.Text = "";
                    txtHeight.Text = "";
                    txtColumn.Text = "";

                    cboStyle.Text = fieSettings[0];
                    cboThambStyle.Text = fieSettings[1];
                    txtWidth.Text = fieSettings[2];
                    txtHeight.Text = fieSettings[3];
                    txtColumn.Text = fieSettings[4];
                }
                catch { }

                try
                {
                    string NewfolderName = open.FileName;
                    string[] sep = { "\\" };
                    string[] words = NewfolderName.Split(sep, StringSplitOptions.None);

                    NewfolderName = "";
                    for (int i = 0; i < words.Length - 1; i++)
                    {
                        if (NewfolderName != "") NewfolderName = NewfolderName + "\\";
                        NewfolderName = NewfolderName + words[i];
                    }

                    //get all files from the selected folder
                    System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(NewfolderName + "\\images");
                    System.IO.FileInfo[] files = dirInfo.GetFiles("*.*");
                    if (files != null)
                    {
                        P.Visible = true; P.Minimum = 0; P.Maximum = files.Length; P.Value = 0;
                        foreach (System.IO.FileInfo file in files)
                        {
                            Application.DoEvents();
                            try
                            {
                                //Checking the file extention
                                string ext = file.Name.Substring(file.Name.Length - 4, 4).ToLower();
                                string ext1 = file.Name.Substring(file.Name.Length - 5, 5).ToLower();
                                if (ext == ".jpg" || ext == ".gif" || ext == ".png" || ext == ".bmp" || ext1 == ".jpeg")
                                {
                                    string FPath = NewfolderName + "\\images\\" + file.Name;

                                    //Add image in the imagelist
                                    imageList1.Images.Add(new Bitmap(FPath));

                                    //Add the image name into the arraylist
                                    string FName = file.Name.ToLower().Replace(".jpg", "").Replace(".gif", "").Replace(".png", "").Replace(".bmp", "").Replace(".jpeg", "");
                                    oImageName.Add(FName);
                                    oImageKey.Add(FPath);

                                    //Add the images into the listview
                                    ListViewItem LITem = lvwImage.Items.Add(FName, imageList1.Images.Count - 1);
                                    LITem.Tag = FPath;
                                }
                            }
                            catch { }
                            P.Value = P.Value + 1;
                        }
                    }
                }
                catch { }
            }
            P.Visible = false;
        }

        private void tlsImgGalary_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            panThambSettings.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panThambSettings.Visible = false;
        }

        private void cboThambStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            picThambst.Visible = true;
            cboAlignment.Enabled = true;
            txtHeight.Enabled = true; txtWidth.Enabled = true;

            label4.Visible = false;
            label5.Visible = false;
            txtMIHeight.Visible = false;
            txtMiWidth.Visible = false;

            if (cboThambStyle.Text.ToLower() == "Androied".ToLower())
            {
                picThambst.Image = Properties.Resources.in_back;
            }
            else if (cboThambStyle.Text.ToLower() == "Dark_Glass".ToLower())
            {
                picThambst.Image = Properties.Resources.bg_back;
            }
            else if (cboThambStyle.Text.ToLower() == "facebook".ToLower())
            {
                picThambst.Image = Properties.Resources.fb_back;
            }
            else if (cboStyle.Text.ToLower() == "iPhone".ToLower())
            {
                picThambst.Image = Properties.Resources.ip_back;
            }
            else if (cboThambStyle.Text.ToLower() == "Mac".ToLower())
            {
                picThambst.Image = Properties.Resources.mc_back;
            }
            else if (cboThambStyle.Text.ToLower() == "Vista".ToLower())
            {
                picThambst.Image = Properties.Resources.vt_back1;
            }
            else if (cboThambStyle.Text.ToLower() == "StickyNotes".ToLower())
            {
                picThambst.Image = Properties.Resources.ct_back;
            }
            else if (cboThambStyle.Text.ToLower() == "Special".ToLower())
            {
                //cboAlignment.Enabled = false;
                //txtHeight.Enabled = false; txtWidth.Enabled = false;
                txtColumn.Text = "7";
                picThambst.Visible = false;

                label4.Visible = true;
                label5.Visible = true;
                txtMIHeight.Visible = true;
                txtMiWidth.Visible = true;
            }
            
        }

        private void txtHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 45 && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.KeyChar = (char)0;
            }
        }

        private void txtWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 45 && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.KeyChar = (char)0;
            }
        }

        private void txtColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 45 && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.KeyChar = (char)0;
            }
        }

        private bool ReGenerateThumbnails(Bitmap source2, string thamb_Name_str_1)
        {
            try
            {
                float _Width = 100; float _Height = 75;
                float _StartX = 5; float _StartY = 5;
                float _StartXORI = 0; float _StartYORI = 0;
                float _WidthDec = 10; float _HeightDec = 10;
                if (txtWidth.Text.Trim() != "") _Width = float.Parse(txtWidth.Text.Trim());
                if (txtHeight.Text.Trim() != "") _Height = float.Parse(txtHeight.Text.Trim());

                Bitmap source1;
                source1 = Properties.Resources.in_back;

                Bitmap source3;
                source3 = Properties.Resources.bg;

                if (cboThambStyle.Text.ToLower() == "Androied".ToLower())
                {
                    source1 = Properties.Resources.in_back;
                    _StartX = (float)((_Width /100 )*8); _StartY = (float)((_Height / 100)*10); _WidthDec = _StartX*2; _HeightDec = _StartY * 2;
                }
                else if (cboThambStyle.Text.ToLower() == "Dark_Glass".ToLower())
                {
                    source1 = Properties.Resources.bg_back;
                    _StartX = (float)((_Width / 100) * 2); _StartY = (float)((_Height / 100) * 2); _WidthDec = _StartX * 2; _HeightDec = _StartY * 2;
                }
                else if (cboThambStyle.Text.ToLower() == "facebook".ToLower())
                {
                    source1 = Properties.Resources.fb_back;
                    _StartX = (float)((_Width / 100) * 7); _StartY = (float)((_Height / 100) * 8); _WidthDec = (_StartX * 2 )+10; _HeightDec = (_StartY * 2)+5;
                }
                else if (cboThambStyle.Text.ToLower() == "iPhone".ToLower())
                {
                    source1 = Properties.Resources.ip_back;
                    _StartX = (float)((_Width / 100) * 8); _StartY = (float)((_Height / 100) * 10); _WidthDec = _StartX * 2; _HeightDec = _StartY * 2;
                }
                else if (cboThambStyle.Text.ToLower() == "Mac".ToLower())
                {
                    source1 = Properties.Resources.mc_back;
                    _StartX = (float)((_Width / 100) * 7); _StartY = (float)((_Height / 100) * 8); _WidthDec = (_StartX * 2) + 20; _HeightDec = (_StartY * 2) + 5;
                }
                else if (cboThambStyle.Text.ToLower() == "Vista".ToLower())
                {
                    source1 = Properties.Resources.vt_back1;
                    _StartX = (float)((_Width / 100) * 6); _StartY = (float)((_Height / 100) * 8); _WidthDec = _StartX * 2; _HeightDec = _StartY * 2;
                }
                else if (cboThambStyle.Text.ToLower() == "StickyNotes".ToLower())
                {
                    source1 = Properties.Resources.ct_back;
                    _StartX = (float)((_Width / 100) * 8); _StartY = (float)((_Height / 100) * 10); _WidthDec = _StartX * 2; _HeightDec = _StartY * 2;
                }

                var target = new Bitmap((int)_Width, (int)_Height, PixelFormat.Format32bppArgb);
                var graphics = Graphics.FromImage(target);
                graphics.CompositingMode = CompositingMode.SourceOver;

                if (cboThambStyle.Text.ToLower() != "Dark_Glass".ToLower())
                {
                    graphics.DrawImage(source3, 0, 0, _Width, _Height);
                }
                graphics.DrawImage(source1, _StartXORI, _StartYORI, _Width - (_StartXORI*2), _Height - (_StartYORI*2));
                graphics.DrawImage(source2, _StartX, _StartY, _Width - _WidthDec, _Height - _HeightDec);
                target.Save(thamb_Name_str_1, ImageFormat.Jpeg);

                return true;
            }
            catch { return false; }
        }
        public string getExtention(string fName)
        {
            string ingExt = fName.Substring(fName.Length - 4, 1);
            if (ingExt == ".")
            {
                ingExt = fName.Substring(fName.Length - 4, 4);
            }
            else
            {
                ingExt = fName.Substring(fName.Length - 5, 1);
                if (ingExt == ".")
                {
                    ingExt = fName.Substring(fName.Length - 5, 5);
                }
                else
                {
                    ingExt = ".jpg";
                }
            }
            return ingExt;            
        }
        #endregion

        private void cboAlignment_Enter(object sender, EventArgs e)
        {
            cboAlignment.Items.Clear();
            if (cboThambStyle.Text.Trim().ToLower() == "Special".ToLower())
            {
                cboAlignment.Items.Add("Left");
                cboAlignment.Items.Add("Right");
                cboAlignment.Items.Add("Top");
                cboAlignment.Items.Add("Buttom");
            }
            else
            {
                cboAlignment.Items.Add("Center");
                cboAlignment.Items.Add("Left");
                cboAlignment.Items.Add("Right");                
            }
        }

        private void txtMIHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 45 && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.KeyChar = (char)0;
            }
        }

        private void txtMiWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 45 && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.KeyChar = (char)0;
            }
        }

        private void cboAlignment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
