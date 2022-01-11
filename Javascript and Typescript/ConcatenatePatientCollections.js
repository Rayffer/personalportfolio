const patientsFloor2 = [
    {
      id: 1,
      name: "John",
      lastname: "Doe",
      sex: "Male",
      temperature: 36.8,
      heartRate: 100,
      specialty: "general medicine",
    },
    {
      id: 2,
      name: "Jane",
      lastname: "Doe",
      sex: "Female",
      temperature: 36.8,
      heartRate: 100,
      specialty: "general medicine",
    },
  ];
  
  const patientsFloor3 = [
    {
      id: 1,
      name: "Mary",
      lastname: "Wien",
      sex: "Female",
      temperature: 36.8,
      heartRate: 100,
      specialty: "general medicine",
    },
    {
      id: 2,
      name: "Scarlett",
      lastname: "Somez",
      sex: "Female",
      temperature: 36.8,
      heartRate: 100,
      specialty: "general medicine",
    },
  ];
  
  const concatPatients = (patientCollectionA, patientCollectionB) => {
    return [...patientCollectionA, ...patientCollectionB]
  };
  
  const totalCollection = concatPatients(patientsFloor2, patientsFloor3);
  
  console.log(patientsFloor2);
  console.log(patientsFloor3);
  console.log(totalCollection);