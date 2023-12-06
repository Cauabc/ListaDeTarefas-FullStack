import styled from "styled-components";

export const Main = styled.main`
    display: flex;
    align-items: center;
    justify-content: center;
    min-height: 100vh;

    .loginForm{
        width: clamp(340px,35vw,35vw);
        height: clamp(400px,73vh,73vh);
        background-color: #21212B;
        border-radius: 12px;
    }

    .topSide{
        width: 100%;
        height: 20vh;
        color: #fff;
        display: flex;
        align-items: center;
        justify-content: center;
        font-size: clamp(1rem, 2vw, 2rem);
        background-color: #252536;
        border-radius: 12px 12px 0 0;
        position: relative;

        &::after{
            content: '';
            position: absolute;
            width: 100%;
            height: 2px;
            background-color: #414141;
            border-radius: 12px 12px 0 0;
            bottom: 0;
        }
    }

    label{
        color: #fff;
        font-weight: 500;
    }

    input{
        background-color: #22222F;
        border: 1px solid #393952;
        height: 3em;
        width: 100%;
        margin-top: 1em;
        margin-bottom: 1em;
        border-radius: 5px;
        outline: 0;
        cursor: text;
        padding: 1em;
        caret-color: #fff;
        color: #fff;
    }

    form{
        display: flex;
        flex-direction: column;
        padding-top: 3vw;
        padding-left: 5vw;
        padding-right: 5vw;
    }

    button{
        background-color: #F95738;
        color: #fff;
        width: clamp(100px, 10vw, 10vw);
        align-self: center;
        margin-top: 1em;
        height: clamp(60px, 8vh, 8vh);
        border-radius: 10px;
        border: 0;
        cursor: pointer;
        font-weight: 500;
        font-size: 1.1em;
        letter-spacing: 1px;
        text-align: center;
    }

    small{
        align-self: center;
        color: #fff;
        margin-top: 10px;
    }

    span{
        cursor: pointer;
        text-decoration: underline;
    }
`