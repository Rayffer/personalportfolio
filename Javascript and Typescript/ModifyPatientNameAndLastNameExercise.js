
{
  const defaultPatient = {
    id: -1,
    name: "",
    lastname: "",
    sex: "Male",
    temperature: 36.8,
    heartRate: 100,
    specialty: "general medicine",
  };

  const createNewPatient = (name, lastname) => {
    return {
      ...defaultPatient,
      name,
      lastname
    }
  };

  const newPatient = createNewPatient("John", "Doe");

  console.log(newPatient);
  console.log(defaultPatient);
}