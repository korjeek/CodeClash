import axios from 'axios';

// Создаем экземпляр Axios
const apiClient = axios.create({
    baseURL: 'https://localhost:7282/', // Замените на ваш базовый URL
    timeout: 10000,
});

// Функция для выполнения post-запроса на обновление токена
const refreshToken = async () => {
    try {
        const response = await axios.post(`${apiClient.defaults.baseURL}/auth/refresh-token`, null, {
            withCredentials: true, // Убедимся, что куки отправляются
        });
        console.log('Token refreshed:', response.data);
        return response.data; // Предполагается, что обновленный токен возвращается
    } catch (error) {
        console.error('Error refreshing token:', error);
        throw error;
    }
};

// Интерсептор ответов
apiClient.interceptors.response.use(
    (response) => {
        console.log('Response:', response);
        return response;
    },
    async (error) => {
        if (error.response && error.response.status === 401) {
            console.log('Unauthorized response detected.');

            // Проверяем наличие куки "spooky-cookies"
            const spookyCookies = document.cookie.split('; ').find((row) => row.startsWith('spooky-cookies='));

            if (spookyCookies) {
                console.log('Spooky-cookies found. Attempting token refresh...');
                try {
                    await refreshToken();
                    // Повторяем исходный запрос
                    return apiClient(error.config);
                } catch (refreshError) {
                    console.error('Failed to refresh token:', refreshError);
                    window.location.href = '/login'; // Перенаправляем на страницу авторизации
                }
            } else {
                console.log('No spooky-cookies found. Redirecting to login...');
                window.location.href = '/login'; // Перенаправляем на страницу авторизации
            }
        }

        return Promise.reject(error);
    }
);

export default apiClient;
