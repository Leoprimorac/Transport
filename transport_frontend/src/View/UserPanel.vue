<template>
    <div class="container userContainer">
        <div class="row userHeader">
            <div class="col">
                <p>Taric Transport Time Management Tool</p>
            </div>
        </div>

            <router-view  name="default"/>
            
            
        <div class="row userBottom">
            <div class="col-6 user">
                <p>User: {{$store.state.user.userName}}</p>
            </div>
            <div class="col-6 date">
                <p>{{currentTime}}</p>
            </div>
        </div>
        <router-view  name="editTimeSheets"/>
 </div>
</template>

<script>
import '../css/User.css';
export default {
    data(){
        return{

            
        }
    },
    computed:{
        currentTime(){
            return this.getDate();
        }
    },  
    methods:{
        getDate(){
            const current = new Date();
            const date = current.getDate()+'.'+(current.getMonth()+1)+'.'+current.getFullYear();
            const time = current.getHours() + ":" + current.getMinutes();
            const dateTime = date +' , '+ time + 'h';
      
        return dateTime;
        }
    }
    
}
</script>