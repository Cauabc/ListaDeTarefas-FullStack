import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import * as S from "./style";

const Register = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleRegister = async (e) => {
    e.preventDefault();
    try {
      await axios.post(
        "https://localhost:7276/api/Auth/register",
        { username, password }
      );
      navigate("/login");
    } catch (ex) {
      console.error(ex);
    }
  };

  return (
    <S.Main>
      <div className="registerForm">
        <div className="topSide">
          <h1>REGISTER</h1>
        </div>
        <form>
          <label htmlFor="Username">Nome de usuário</label>
          <input
            type="text"
            name="Username"
            id="Username"
            required
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
          <label htmlFor="Password">Senha</label>
          <input
            type="password"
            name="Password"
            id="Password"
            required
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <button type="submit" onClick={(e) => handleRegister(e)}>
            Criar conta
          </button>
          <small>Já tem uma conta? <span onClick={() => navigate("/login")}>Logar.</span></small>
        </form>
      </div>
    </S.Main>
  );
};

export default Register;
