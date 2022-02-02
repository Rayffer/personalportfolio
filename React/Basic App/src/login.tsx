import React from 'react';
import { Link, useNavigate } from 'react-router-dom';

export const LoginPage: React.FC = () => {
    const navigate = useNavigate();
    const [username, setUsername] = React.useState('');
    const [password, setPassword] = React.useState('');

    const handleNavigation = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (username === 'admin' && password === 'admin') {
            navigate('/list');
        } else {
            alert('User/Password is incorrect');
        }
    };

    return (
        <>
            <form onSubmit={handleNavigation}>
                <div className='login-container'>
                    <input
                        placeholder='Username'
                        value={username}
                        onChange={e => setUsername(e.target.value)}
                    />
                    <input
                        placeholder='Password'
                        type="password"
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                    />
                    <button
                        type='submit'>Login
                    </button>
                </div>
            </form>
        </>
    );
};