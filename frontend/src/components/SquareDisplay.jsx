import React, { useEffect, useState } from "react";
import { GetSquareMessage, AddSquare } from "../api/squareApi";
import AddButton from "./AddButton";
import Square from "./Square";

export default function SquareDisplay() {
  const [message, setMessage] = useState("");
  const [squares, setSquares] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function fetchMessage() {
      const result = await GetSquareMessage();
      setMessage(result);
      setSquares(result.squares || []);
      setLoading(false);
    }
    fetchMessage();
  }, []);

  const handleAddSquare = async () => {
  const newSquare = await AddSquare();
  setSquares(prev => [...prev, newSquare]);
};

 return (
  <div 
    style={{
      display: "flex", 
      flexDirection: "column", 
      alignItems: "center",   
      backgroundColor: "#2961bbff", 
      height: "100vh", 
      width: "100%",
    }}>
    <AddButton onClick={handleAddSquare}/>
    <div 
      style={{ 
        position: "absolute",
        alignItems: "center",
        width: "50%",
        height: "50%",
        top: "30%",
        left: "40%",
      }}
    >
       {squares.map(square => (
      <Square 
        x={square.x}
        y={square.y}
        lablel={square.label}
      />
       ))}
    </div>
  </div>
 );
}