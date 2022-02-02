export const doLogin = (
    username: string, 
    password: string
    ): Promise<Boolean> => {
        const promise = new Promise<Boolean>((resolve, reject) => {
            setTimeout(() => resolve(username === 'admin' && password === 'admin'))
        });

        return promise;
    };
    