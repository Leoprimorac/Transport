<template>
    <div class="container">
        <div class="row header">
            <div class="col">
                <p>Taric Transport Time Management Tool</p>
            </div>
        </div>

            <router-view  name="default"/>
            
            
        <div class="row bottom">
            <div class="col-6 user">
                <p>User: {{$store.state.user.userName}}</p>
            </div>
            <div class="col-6 date">
                <p>{{getDate()}}</p>
            </div>
        </div>
 </div>
</template>

<script>
import '../css/Admin.css';
export default {
    data(){
        return{

            
        }
    },
    computed:{
        user(){
            return console.log(this.$store.state.user);
        },
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