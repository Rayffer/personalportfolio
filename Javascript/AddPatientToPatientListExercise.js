{
    const patients = [
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

    const newPatient = {
        id: 2,
        name: "Junior",
        lastname: "Doe",
        sex: "Male",
        temperature: 36.8,
        heartRate: 100,
        specialty: "pediatrics",
    };

    const addPatient = (newPatient, patients) => {
        return [...patients, newPatient]
    };

    const newPatientCollection = addPatient(newPatient, patients);

    console.log(patients);
    console.log(newPatientCollection);
}