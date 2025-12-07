
export async function GetSquareMessage() {
  try {
    const response = await fetch("http://localhost:5205/api/square/GetMemory"); 
    if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
    const data = await response.json();
    return data;
  } catch (error) {
    console.error("Error fetching backend:", error);
    return "Failed to fetch backend";
  }
}

export async function AddSquare() {
  try {
    const response = await fetch("http://localhost:5205/api/square/AddNewSquare", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      }
    });
    if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
    const data = await response.json();  
    return data;
  } catch (error) {
    console.error("Error fetching backend:", error);
    return { error: "Failed to fetch backend" };
  }
}