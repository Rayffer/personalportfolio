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

  const updatePatientInmutable = (fieldId, fieldValue, patient) => {
    return {
      ...patient,
      [fieldId]: fieldValue
    }
  };

  const newPatient = updatePatientInmutable("lastname", "Doe", defaultPatient);

  console.log(newPatient);
  console.log(defaultPatient);
}