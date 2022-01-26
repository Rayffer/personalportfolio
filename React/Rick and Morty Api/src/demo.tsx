import React from "react";

export const MyComponent = () => {
  const [filter, setFilter] = React.useState("");
  const [userCollection, setUserCollection] = React.useState([]);

  React.useEffect(() => {
    fetch(`https://rickandmortyapi.com/api/character${filter === "" ? "" : `?name=${filter}`}`)
      .then((response) => 
      {
        if (response.status === 404) 
        {
          return setUserCollection([]);
        }
        else
        {
          return response.json();
        }
      })
      .then((json) => setUserCollection(json["results"]));
  }, [filter]);
  
  return (
    <div>
      <input value={filter} onChange={(e) => setFilter(e.target.value)} />
      <div className="character-list">
        {userCollection.map((user) => (
            <>
                <div className="character-container">
                    <img src={user.image} />
                    <div key={user.name}>{user.name}</div>
                </div>
            </>
        ))}
      </div>
    </div>
  );
};