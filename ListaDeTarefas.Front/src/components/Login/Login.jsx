import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import * as S from "./style";

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post(
        "https://localhost:7276/api/Auth/login",
        { username, password }
      );
      localStorage.setItem("token", response.data.tokenString);
      localStorage.setItem("userid", response.data.userId);
      localStorage.setItem("username", response.data.username);
      navigate("/");
    } catch (ex) {
      console.error(ex);
    }
  };

  return (
    <S.Main>
      <div className="loginForm">
        <div className="topSide">
          <h1>LOGIN</h1>
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
          <button type="submit" onClick={(e) => handleLogin(e)}>
            Entrar
          </button>
          <small>Não tem uma conta? <span onClick={() => navigate("/register")}>Criar.</span></small>
        </form>
      </div>
    </S.Main>
  );
};

export default Login;
