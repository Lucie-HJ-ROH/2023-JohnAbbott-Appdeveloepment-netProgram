﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace MidtermTravel
{
    public class Trip
    {
        public Trip() { }
        public Trip(string destination, string name, string passport, DateTime departureDate, DateTime returnDate)
        {
            Destination = destination;
            TravlerName = name;
            TravlerPassport= passport;
//            DepartureDate = departureDate;
//            ReturnDate = returnDate;
            
            
             SetDepartureReturnDates(departureDate, returnDate); 


        }

        public int Id { get; set; }


        //Destination
        private string _destination;

        [Required]
        [StringLength(30)]
        public string Destination
        {
            get
            {
                return _destination;
            }
            set
            {          
                if (value.Length < 2 || value.Length > 31)
                {
                    throw new ArgumentException("Destination length must be 2-30 characters long");
                }
                _destination = value;
            }
        }


        //TravlersName
        private string _travlerName;

        [Required]
        [StringLength(30)]
        public string TravlerName
        {
            get
            {
                return _travlerName;
            }
            set
            {
                if (value.Length < 2 || value.Length > 31)
                {
                    throw new ArgumentException("Travler's name length must be 2-30 characters long");
                }
                _travlerName = value;
            }
        }

        //TravlersPassport
        private string _travlerPassport;

        [Required]
        [StringLength(30)]
        public string TravlerPassport
        {
            get
            {
                return _travlerPassport;
            }
            set
            {
                //^[A - Z]{2}[0-9]{6}$   -match upper characters followed by 6 digits
                

                if (!string.IsNullOrEmpty(_travlerPassport))
                {
                    if (!Regex.IsMatch(_travlerPassport, @"^[A-Z]{2}[0-9]{6}$")) {
                        throw new ArgumentException("Travler's passport must be started  with 2 upper caracters.");
                    }
                
                      
                }
                _travlerPassport = value;



            }
        }

        //Departure date
        private DateTime _departureDate;


        [Required]
        [DataType(DataType.Date)]
        // [DisplayFormat(ApplyFormatInEditMode = true)] //, DataFormatString = "{yyyy/MM/0:dd}")]
        public DateTime DepartureDate
        {
            get
            {
                return _departureDate;
            }
/*
            set
            {
                if (value.Year < 1900 || value.Year > 2099)
                {
                    throw new ArgumentException("Invalid year. Must be between 1900-2099.");
                }
                _departureDate = value;
            }
*/
        }

        //Return date
        private DateTime _returnDate;


        [Required]
        [DataType(DataType.Date)]
        // [DisplayFormat(ApplyFormatInEditMode = true)] //, DataFormatString = "{yyyy/MM/0:dd}")]
        public DateTime ReturnDate
        {
            get
            {
                return _returnDate;
            }
        /*    
            set
            {


                if ((value.Year < 1900 || value.Year > 2099) &&( _departureDate.Year > value.Year && _departureDate.Month > value.Month && _departureDate.Date > value.Date))
                {
                    throw new ArgumentException("Invalid year. Must be between 1900-2099.");
                }
                _returnDate = value;
            }
        */
            
        }

        public void SetDepartureReturnDates(DateTime dep, DateTime ret) {


            if (dep > ret)
            {
                throw new ArgumentException("Return date must be later than department date");
               
            }
            if (dep.Year < 1900 || dep.Year > 2099 || ret.Year < 1900 || ret.Year > 2099) {
                throw new ArgumentException("Invalid year. Must be between 1900-2099.");
                

            }
            _departureDate = dep;
            _returnDate = ret;
           
        }

    }
}
