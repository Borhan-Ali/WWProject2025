import React from "react";

export default function AddButton({ onClick }) {
  return (
    <button
      onClick={onClick}
      style={{
      
        padding: "3vh 9vh",
        fontSize: "3vh",
        color: "white",
        border: "2px solid black",
        cursor: "pointer",
        borderRadius: "5%",
        marginTop: "40%", 
        backgroundColor: "#6741cfff"
      }}
    >
      Lägg till ruta
    </button>
  );
}