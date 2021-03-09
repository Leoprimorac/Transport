import Vue from 'vue'
import Router from 'vue-router'
Vue.use(Router)

const MainMenu = () => import('./components/AdminComponents/MainMenu.vue')
const manageUser = () => import('./components/AdminComponents/ManageUsers.vue')
const CreateUser = () => import('./components/AdminComponents/CreateUser.vue')
const editUser = () => import('./components/AdminComponents/EditUser.vue')
const timeSheet = () => import('./components/AdminComponents/TimeSheets.vue')
const auditFiles = () => import('./components/AdminComponents/AuditFile.vue')

const UserPanel = () => import('./View/UserPanel')
const Index = () => import('./View/Index')
const AdminPanel = () => import('./View/AdminPanel')
const UserMenu = () => import('./components/UserComponents/MainMenu.vue')
const editTimeSheet = () => import('./components/UserComponents/EditTimeSheet.vue')
const openTimeSheet = () => import('./components/UserComponents/TimeSheets.vue')
const exportTimeSheets = () => import('./components/AdminComponents/ExportTimeSheets.vue')

export default new Router({

    mode: "history",
    base: process.env.BASE_URL,
    routes: [

        {
            path: '/',
            name: 'Index',
            component: Index
        },
        {
            path: '/Admin',
            name: 'Admin',
            component: AdminPanel,
            props: true,
            children: [
                {
                    path: 'createUser',
                    name: 'createUser',
                    components: {
                        default: CreateUser
                    }
                },
                {
                    path: 'mainMenu',
                    name: 'mainMenu',
                    components: {
                        default: MainMenu
                    },

                },
                {
                    path: 'manageUser',
                    name: 'manageUser',
                    components: {
                        default: manageUser
                    },

                },
                {
                    path: 'editUser',
                    name: 'editUser',
                    prop: true,
                    components: {
                        default: editUser
                    },

                },
                {
                    path: 'timeSheet',
                    name: 'timeSheet',
                    components: {
                        default: timeSheet
                    },

                },
                {
                    path: 'auditFiles',
                    name: 'auditFiles',
                    components: {
                        default: auditFiles
                    },

                },
                {
                    path: 'exportTimeSheets',
                    name: 'exportTimeSheets',
                    components: {
                        default: exportTimeSheets
                    },

                },
            ]
        },
        {
            path: '/User',
            name: 'User',
            component: UserPanel,
            children: [
                {
                    path: 'mainMenu',
                    name: 'mainMenu',
                    components: {
                        default: UserMenu
                    },
                },
                {
                    path: 'editTimeSheet',
                    name: 'editTimeSheet',
                    components: {
                        editTimeSheets: editTimeSheet
                    },
                },
                {
                    path: 'openTimeSheet',
                    name: 'openTimeSheet',
                    components: {
                        editTimeSheets: openTimeSheet
                    },
                }
            ]
        }
    ]
});