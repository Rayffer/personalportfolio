{
  const defaultPatient = {
    id: -1,
    name: "",
    lastname: "",
    sex: "Male",
    sensors: {
      temperature: 36.8,
      heartRate: 100,
    },
    specialty: "general medicine",
  };

  const createNewPatient = (temperature) => {
    return {
      ...defaultPatient,
      sensors: {
        ...defaultPatient.sensors,
        temperature,
      }
    }
  };

  const newPatient = createNewPatient(38);

  console.log(newPatient);
  console.log(defaultPatient);
}