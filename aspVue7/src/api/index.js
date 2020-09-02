import request from '../utils/request';

export const fetchData = query => {
    return request({
        url: 'C:/Users/sexiao/Desktop/WebAPP/ASP.netcore3.0-VUE.JS/aspVue7/src/api/table.json',
        method: 'get',
        params: query
    });
};
