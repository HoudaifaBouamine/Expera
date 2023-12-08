import React from 'react'
import './Style/index.css'
import CTA_Button from "../compenenet/CTA_Button"
import SecondaryButton from "../compenenet/SecondaryButton"

const navBar = () => {
  return (
   <div className ="navBar">
        <h1 className="Logo">LOGO</h1>
      <div className='navBar-button'>
        <CTA_Button text={"Get Started"}/>
        <SecondaryButton text={"Log in"}/>
      </div>
    </div>
    
  );
}

export default navBar