// This file allows you to configure ESLint according to your project's needs, so that you
// can control the strictness of the linter, the plugins to use, and more.

// For more information about configuring ESLint, visit https://eslint.org/docs/user-guide/configuring/
import apiClient from "./AxiosClient";


// GET all accounts
export const getChartsOfAccounts = async () => {
    const response = await apiClient.get(
        "/gl-accounts"
    );

    return response.data;
};


// GET one account
export const getChartOfAccount = async (id) => {
    const response = await apiClient.get(
        `/gl-accounts/${id}`
    );

    return response.data;
};


// CREATE account
export const createChartOfAccount = async (data) => {
    const response = await apiClient.post(
        "/gl-accounts",
        data
    );

    return response.data;
};


// UPDATE account
export const updateChartOfAccount = async (id, data) => {
    const response = await apiClient.put(
        `/gl-accounts/${id}`,
        data
    );

    return response.data;
};


// DELETE account
export const deleteChartOfAccount = async (id) => {
    const response = await apiClient.delete(
        `/gl-accounts/${id}`
    );

    return response.data;
};