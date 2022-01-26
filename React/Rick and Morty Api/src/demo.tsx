import React from "react";

export const MyComponent = () => {
  const [filter, setFilter] = React.useState("");
  const [userCollection, setUserCollection] = React.useState({results: []});

  React.useEffect(() => {
    fetch(`https://rickandmortyapi.com/api/character`)
      .then((response) => response.json())
      .then((json) => setUserCollection(json));
  }, [filter]);
  
  return (
    <div>
      <input value={filter} onChange={(e) => setFilter(e.target.value)} />
      <div className="character-list">
        {userCollection.results.filter((user) => user.name.toLowerCase().includes(filter.toLowerCase())).map((user) => (
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