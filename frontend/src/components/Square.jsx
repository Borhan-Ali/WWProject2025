import React from "react";

export default function Square({x,y, lablel}) {
  return (
    <div
      style={{
        width: "42px",
        height: "42px",
        backgroundColor: lablel,
        borderRadius: "4px",
        position: "absolute",
        border: "2px solid black",

        left:  (x * 50) + 'px',
        top:   (y * 50) + 'px',
      }}
    />
  );
}