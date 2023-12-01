import React, { useRef } from "react";
import "./style.css";
import { HiUserCircle } from "react-icons/hi2";
const Header = () => {

  const modalRef = useRef(null);

  const handleUserAction = () => {
    modalRef.current.classList.toggle("active");
  }

  return (
    <>  
      <header>
        <p>Olá, Lourival</p>
        <HiUserCircle className="userIcon" onClick={handleUserAction}/>
      </header>
      <div className="modalUserAction" ref={modalRef}>
        <p>Sair</p>
      </div>
    </>
  );
};

export default Header;
