import React, { useRef } from "react";
import "./style.css";
import { HiUserCircle } from "react-icons/hi2";
import * as S from './style'
import { useNavigate } from 'react-router-dom'

const Header = () => {
  const navigate = useNavigate();
  const modalRef = useRef(null);

  const handleUserAction = () => {
    modalRef.current.classList.toggle("active");
  }

  const handleLogout = () => {
    localStorage.clear();
    navigate("/login")
  }

  return (
    <>  
      <S.Header>
        <p>Olá, {localStorage.getItem("username")}</p>
        <HiUserCircle className="userIcon" onClick={handleUserAction}/>
      </S.Header>
      <div className="modalUserAction" ref={modalRef}>
        <p onClick={handleLogout}>Sair</p>
      </div>
    </>
  );
};

export default Header;
