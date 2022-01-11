{
    const patients = [
        {
            id: 1,
            name: "John",
            lastname: "Doe",
            sex: "Male",
            temperature: 36.8,
            heartRate: 80,
            specialty: "pediatrics",
            age: 44
        },
        {
            id: 2,
            name: "Jane",
            lastname: "Doe",
            sex: "Female",
            temperature: 36.8,
            heartRate: 70,
            specialty: "pediatrics",
            age: 43
        },
        {
            id: 3,
            name: "Junior",
            lastname: "Doe",
            sex: "Male",
            temperature: 36.8,
            heartRate: 90,
            specialty: "pediatrics",
            age: 8
        },
        {
            id: 4,
            name: "Mary",
            lastname: "Wien",
            sex: "Female",
            temperature: 36.8,
            heartRate: 120,
            specialty: "general medicine",
            age: 20
        },
        {
            id: 5,
            name: "Scarlett",
            lastname: "Somez",
            sex: "Female",
            temperature: 36.8,
            heartRate: 110,
            specialty: "general medicine",
            age: 30
        },
        {
            id: 6,
            name: "Brian",
            lastname: "Kid",
            sex: "Male",
            temperature: 39.8,
            heartRate: 80,
            specialty: "pediatrics",
            age: 11
        }
    ];

    let computedPatients = { pediatricsPatients: 0, generalMedicinePatients: 0 };
    let computedGeneralMedicinePatients = patients.reduce(
        (computedPatients, patient) => {
            switch (patient.specialty) {
                case "general medicine":
                    computedPatients.generalMedicinePatients++;
                    break;

                case "pediatrics":
                    computedPatients.pediatricsPatients++;
                    break;
            }

            return computedPatients;
        },
        computedPatients
    );

    console.log(computedPatients);
}