import React, { useState, useEffect } from 'react';
import apiConfig from '../services/apiConfig';
import '../css/bootstrap.min.css';
import '../css/font-awesome.min.css';
import '../css/bootstrap.min.css.map';
import '../css/elegant-icons.css';
import '../css/flaticon.css';
import '../css/owl.carousel.min.css';
import '../css/nice-select.css';
import '../css/jquery-ui.min.css';
import '../css/magnific-popup.css';
import '../css/slicknav.min.css';
import '../css/style.css';


function Home() {
  return (
    <div>
      {/* Offcanvas Menu Section Begin */}
      <div className="offcanvas-menu-overlay"></div>
      <div className="canvas-open">
        <i className="icon_menu"></i>
      </div>
      <div className="offcanvas-menu-wrapper">
        <div className="canvas-close">
          <i className="icon_close"></i>
        </div>
        <div className="search-icon search-switch">
          <i className="icon_search"></i>
        </div>
        <div className="header-configure-area">
          <div className="language-option">
            <img src="img/flag.jpg" alt="" />
            <span>EN <i className="fa fa-angle-down"></i></span>
            <div className="flag-dropdown">
              <ul>
                <li><a href="#">Zi</a></li>
                <li><a href="#">Fr</a></li>
              </ul>
            </div>
          </div>
          <a href="#" className="bk-btn">Booking Now</a>
        </div>
        <nav className="mainmenu mobile-menu">
          <ul>
            <li className="active"><a href="./index.html">Home</a></li>
            <li><a href="./rooms.html">Rooms</a></li>
            <li><a href="./about-us.html">About Us</a></li>
            <li><a href="./pages.html">Pages</a>
              <ul className="dropdown">
                <li><a href="./room-details.html">Room Details</a></li>
                <li><a href="#">Deluxe Room</a></li>
                <li><a href="#">Family Room</a></li>
                <li><a href="#">Premium Room</a></li>
              </ul>
            </li>
            <li><a href="./blog.html">News</a></li>
            <li><a href="./contact.html">Contact</a></li>
          </ul>
        </nav>
        <div id="mobile-menu-wrap"></div>
        <div className="top-social">
          <a href="#"><i className="fa fa-facebook"></i></a>
          <a href="#"><i className="fa fa-twitter"></i></a>
          <a href="#"><i className="fa fa-tripadvisor"></i></a>
          <a href="#"><i className="fa fa-instagram"></i></a>
        </div>
        <ul className="top-widget">
          <li><i className="fa fa-phone"></i> (12) 345 67890</li>
          <li><i className="fa fa-envelope"></i> info.colorlib@gmail.com</li>
        </ul>
      </div>
      {/* Offcanvas Menu Section End */}

      {/* Header Section Begin */}
      <header className="header-section">
        <div className="top-nav">
          <div className="container">
            <div className="row">
              <div className="col-lg-6">
                <ul className="tn-left">
                  <li><i className="fa fa-phone"></i> (12) 345 67890</li>
                  <li><i className="fa fa-envelope"></i> info.colorlib@gmail.com</li>
                </ul>
              </div>
              <div className="col-lg-6">
                <div className="tn-right">
                  <div className="top-social">
                    <a href="#"><i className="fa fa-facebook"></i></a>
                    <a href="#"><i className="fa fa-twitter"></i></a>
                    <a href="#"><i className="fa fa-tripadvisor"></i></a>
                    <a href="#"><i className="fa fa-instagram"></i></a>
                  </div>
                  <a href="#" className="bk-btn">Booking Now</a>
                  <div className="language-option">
                    <img src="img/flag.jpg" alt="" />
                    <span>EN <i className="fa fa-angle-down"></i></span>
                    <div className="flag-dropdown">
                      <ul>
                        <li><a href="#">Zi</a></li>
                        <li><a href="#">Fr</a></li>
                      </ul>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div className="menu-item">
          <div className="container">
            <div className="row">
              <div className="col-lg-2">
                <div className="logo">
                  <a href="./index.html">
                    <img src="img/logo.png" alt="" />
                  </a>
                </div>
              </div>
              <div className="col-lg-10">
                <div className="nav-menu">
                  <nav className="mainmenu">
                    <ul>
                      <li className="active"><a href="./index.html">Home</a></li>
                      <li><a href="./rooms.html">Rooms</a></li>
                      <li><a href="./about-us.html">About Us</a></li>
                      <li><a href="./pages.html">Pages</a>
                        <ul className="dropdown">
                          <li><a href="./room-details.html">Room Details</a></li>
                          <li><a href="./blog-details.html">Blog Details</a></li>
                          <li><a href="#">Family Room</a></li>
                          <li><a href="#">Premium Room</a></li>
                        </ul>
                      </li>
                      <li><a href="./blog.html">News</a></li>
                      <li><a href="./contact.html">Contact</a></li>
                    </ul>
                  </nav>
                  <div className="nav-right search-switch">
                    <i className="icon_search"></i>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </header>
      {/* Header End */}

      {/* Hero Section Begin */}
      <section className="hero-section">
        <div className="container">
          <div className="row">
            <div className="col-lg-6">
              <div className="hero-text">
                <h1>Sona A Luxury Hotel</h1>
                <p>Here are the best hotel booking sites, including recommendations for international travel and for finding low-priced hotel rooms.</p>
                <a href="#" className="primary-btn">Discover Now</a>
              </div>
            </div>
            <div className="col-xl-4 col-lg-5 offset-xl-2 offset-lg-1">
              <div className="booking-form">
                <h3>Booking Your Hotel</h3>
                <form action="#">
                  <div className="check-date">
                    <label htmlFor="date-in">Check In:</label>
                    <input type="text" className="date-input" id="date-in" />
                    <i className="icon_calendar"></i>
                  </div>
                  <div className="check-date">
                    <label htmlFor="date-out">Check Out:</label>
                    <input type="text" className="date-input" id="date-out" />
                    <i className="icon_calendar"></i>
                  </div>
                  <div className="select-option">
                    <label htmlFor="guest">Guests:</label>
                    <select id="guest">
                      <option value="">2 Adults</option>
                      <option value="">3 Adults</option>
                    </select>
                  </div>
                  <div className="select-option">
                    <label htmlFor="room">Room:</label>
                    <select id="room">
                      <option value="">1 Room</option>
                      <option value="">2 Room</option>
                    </select>
                  </div>
                  <button type="submit">Check Availability</button>
                </form>
              </div>
            </div>
          </div>
        </div>
        <div className="hero-slider owl-carousel">
  <div className="hs-item set-bg" style={{ backgroundImage: "url('img/hero/hero-1.jpg')" }}></div>
  <div className="hs-item set-bg" style={{ backgroundImage: "url('img/hero/hero-2.jpg')" }}></div>
  <div className="hs-item set-bg" style={{ backgroundImage: "url('img/hero/hero-3.jpg')" }}></div>
</div>

      </section>
      {/* Hero Section End */}

      {/* Footer Section Begin */}
      <footer className="footer-section">
        <div className="container">
          <div className="footer-text">
            <div className="row">
              <div className="col-lg-4">
                <div className="ft-about">
                  <div className="logo">
                    <a href="#">
                      <img src="img/footer-logo.png" alt="" />
                    </a>
                  </div>
                  <p>We inspire and reach millions of travelers<br /> across 90 local websites</p>
                  <div className="fa-social">
                    <a href="#"><i className="fa fa-facebook"></i></a>
                    <a href="#"><i className="fa fa-twitter"></i></a>
                    <a href="#"><i className="fa fa-tripadvisor"></i></a>
                    <a href="#"><i className="fa fa-instagram"></i></a>
                    <a href="#"><i className="fa fa-youtube-play"></i></a>
                  </div>
                </div>
              </div>
              <div className="col-lg-3 offset-lg-1">
                <div className="ft-contact">
                  <h6>Contact Us</h6>
                  <ul>
                    <li>(12) 345 67890</li>
                    <li>info.colorlib@gmail.com</li>
                    <li>856 Cordia Extension Apt. 356, Lake, United State</li>
                  </ul>
                </div>
              </div>
              <div className="col-lg-3 offset-lg-1">
                <div className="ft-newslatter">
                  <h6>New latest</h6>
                  <p>Get the latest updates and offers.</p>
                  <form action="#" className="fn-form">
                    <input type="text" placeholder="Email" />
                    <button type="submit"><i className="fa fa-send"></i></button>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      </footer>
      {/* Footer Section End */}
    </div>
  );
}

export default Home;
