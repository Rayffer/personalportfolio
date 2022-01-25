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

    function groupBySpecialty(acumulator, currentPatient) {
        if (!acumulator[currentPatient.specialty]) {
            acumulator[currentPatient.specialty] = 0;
        }
        return {
            ...acumulator,
            [currentPatient.specialty]: (acumulator[currentPatient.specialty] | 0) + 1
        };
    }

    const computedPatients = patients.reduce(
        (acumulator, patient) => groupBySpecialty(acumulator, patient),
        {}
    );

    console.log(computedPatients);