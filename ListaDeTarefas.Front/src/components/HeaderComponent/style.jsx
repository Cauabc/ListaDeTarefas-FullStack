import styled from "styled-components";

export const Header = styled.header`
    position: relative;
    width: 100vw;
    display: flex;
    justify-content: flex-end;
    align-items: center;
    background-color: #21212B;
    height: 8vh;
    min-height: 50px;
    gap: 10px;
    padding: 0 2em;
    box-shadow: 1px 1px 7px #000;
    z-index: 2;

    p{
        color: #fff;
    }

    .userIcon {
        cursor: pointer;
        width: 40px;
        height: 40px;
    }
`