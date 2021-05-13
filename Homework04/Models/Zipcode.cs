using System;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace Homework04.Models
{
    class Zipcode : IDataErrorInfo, INotifyPropertyChanged
    {
        private string name = string.Empty;
        private string nameError;
        string us_zip = @"^\d{5}(?:[-\s]\d{4})?$";
        string can_zip = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";
        static bool EverythingGood = false;

        public string ZipCodeError
        {
            get
            {
                return nameError;
            }
            set
            {
                if (nameError != value)
                {
                    nameError = value;
                    OnPropertyChanged("ZipCodeError");
                }
            }
        }

        public bool ZipCodeIsCorrect
        {
            get { return EverythingGood; }
        }

        public string ZpCode
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("ZpCode");
                }
            }
        }

        // IDataErrorInfo interface
        public string Error
        {
            get
            {
                return ZipCodeError;
            }
        }

        // 
        // use Regex to authenticate if the entered zip code or 
        // postal-code is valid or not
        private bool ValidUSZipCodeOrCanadianPostalCode(string zip)
        {
            if ((!Regex.Match(zip, us_zip).Success) && (!Regex.Match(zip, can_zip).Success))
                return false;
            return true;
        }

        // IDataErrorInfo interface
        // Called when ValidatesOnDataErrors=True
        public string this[string columnName]
        {
            get
            {
                ZipCodeError = "";
                EverythingGood = false;
                string zipcode = ZpCode.ToUpper();
                int length = zipcode.Length;
                int dashLoc = zipcode.IndexOf('-');

                switch (columnName)
                {
                    case "ZpCode":
                        {
                            if (ValidUSZipCodeOrCanadianPostalCode(zipcode))
                            {
                                EverythingGood = true;
                            }
                            else if (string.IsNullOrEmpty(zipcode))
                            {
                                ZipCodeError = "Zip Code or Postal-Code cannot be empty.";
                            }

                            else if (length == 5 || length == 10)
                            {
                                if (length == 5 && !int.TryParse(zipcode, out int n))
                                {
                                    ZipCodeError = "Zip Code must be all numeric characters.";
                                }
                                else if (dashLoc > 0)
                                {
                                    string first_part = zipcode.Split('-')[0];
                                    if (first_part.Length != 5)
                                    {
                                        ZipCodeError = "First part of Zip Code must consist of 5 digits.";
                                    }

                                    if (!int.TryParse(first_part, out int mn))
                                    {
                                        ZipCodeError = "Zip Code must be all numeric characters.";
                                    }

                                    string second_part = zipcode.Split('-')[1];
                                    if (second_part.Length != 4)
                                    {
                                        ZipCodeError = "Second part of Zip Code must consist of 4 digits.";
                                    }

                                    if (!int.TryParse(second_part, out int m))
                                    {
                                        ZipCodeError = "Zip Code must be all numeric characters.";
                                    }
                                }
                            }
                            else if (!int.TryParse(zipcode, out int n) && length > 6)
                            {
                                ZipCodeError = "Canadian Postal Code cannot be more than 6 alpha-numeric characters.";
                            }
                            else
                            {
                                ZipCodeError = "Invalid Zip Code or Pastal C ode.";
                            }

                            break;
                        }
                }
                return ZipCodeError;
            }
        }

        // INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
