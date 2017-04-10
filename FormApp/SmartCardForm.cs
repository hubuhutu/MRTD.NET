﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DemoApp;
using SmartCardApi.Infrastructure;
using SmartCardApi.MRZ;

namespace FormApp
{
    public partial class SmartCardForm : Form
    {
        public SmartCardForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.cardNumberForMRZTexBox.Text = "12IB34415";
            this.dateOfBirthDateTimePicker.Value = new DateTime(1992, 06, 16);
            this.dateOfExpiryDateTimePicker.Value = new DateTime(2022, 10, 08);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            this.readBtn.Enabled = false;
            var mrzInfo = new MRZInfo(
                this.cardNumberForMRZTexBox.Text,
                this.dateOfBirthDateTimePicker.Value,
                this.dateOfExpiryDateTimePicker.Value
            );
            var dgsContent = await new SmartCardContent(mrzInfo)
                .Content();

            var firstName = dgsContent.Dg1Content.MRZ.NameOfHolder
                .Replace("<", " ")
                .Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)
                .Last();
            var lastName = dgsContent.Dg1Content.MRZ.NameOfHolder
                .Replace("<", " ")
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .First();
            faceImagePictureBox.Image = new Bitmap(
                                    Image
                                        .FromStream(
                                            new MemoryStream(dgsContent.Dg2Content.FaceImageData)
                                        ),
                                    128, 128
                                );
            this.signatureImagePictureBox.Image = new Bitmap(
                Image
                    .FromStream(
                        new MemoryStream(dgsContent.Dg7Content.DisplayedSignatureImageData)
                    )
            );

            this.firstNameTextBox.Text = firstName;
            this.lastNameTextBox.Text = lastName;
            this.citTextBox.Text = dgsContent.Dg1Content.MRZ.Nationality;
            this.sexTextBox.Text = dgsContent.Dg1Content.MRZ.Sex;
            this.personalNumberTextBox.Text = dgsContent.Dg11Content.PersonalNumber;
            this.dateOfBirthTextBox.Text = dgsContent.Dg1Content.MRZ.DateOfBirth.ToShortDateString();
            this.dateOfExpiryTextBox.Text = dgsContent.Dg1Content.MRZ.DateOfExpiry.ToShortDateString();
            this.cardNumberTextBox.Text = dgsContent.Dg1Content.MRZ.DocumentNumber;


            this.readBtn.Enabled = true;
        }

        private void personalNumberTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void sexTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}