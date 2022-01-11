{
    const patients = [
        {
            id: 1,
            name: 'John',
            lastname: 'Doe',
            sex: 'Male',
            temperature: 36.8,
            heartRate: 80,
            specialty: 'general medicine',
            age: 44,
        },
        {
            id: 2,
            name: 'Jane',
            lastname: 'Doe',
            sex: 'Female',
            temperature: 36.8,
            heartRate: 70,
            specialty: 'general medicine',
            age: 43,
        },
        {
            id: 3,
            name: 'Junior',
            lastname: 'Doe',
            sex: 'Male',
            temperature: 36.8,
            heartRate: 90,
            specialty: 'pediatrics',
            age: 8,
        },
        {
            id: 4,
            name: 'Mary',
            lastname: 'Wien',
            sex: 'Female',
            temperature: 36.8,
            heartRate: 120,
            specialty: 'general medicine',
            age: 20,
        },
        {
            id: 5,
            name: 'Scarlett',
            lastname: 'Somez',
            sex: 'Female',
            temperature: 36.8,
            heartRate: 110,
            specialty: 'general medicine',
            age: 30,
        },
        {
            id: 6,
            name: 'Brian',
            lastname: 'Kid',
            sex: 'Male',
            temperature: 39.8,
            heartRate: 80,
            specialty: 'pediatrics',
            age: 11,
        },
    ];

    const patientsAsignedToPediatrics = (patientsToFilter) => {
        return patientsToFilter.filter(patient => patient.specialty === 'pediatrics');
    };

    const patientsBelow10Years = (pediatricsPatientsToFilter) => {
        return pediatricsPatientsToFilter.filter(patient => patient.age < 10);
    };


    const pediatricPatients = patientsAsignedToPediatrics(patients);
    const pediatricPatientsBelow10Years = patientsBelow10Years(pediatricPatients);

    console.log(patients);
    console.log(pediatricPatients);
    console.log(pediatricPatientsBelow10Years);
}