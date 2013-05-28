function [ predictions ] = forecast_road_speed( training_data, real_data )

%% constants declaration
backward_data_usage_depth = 4; % app will use actual data this no backward
road_you_need_speed_on = 3;
how_far_forward_to_forecast = 3;
minutes_per_step = 5;

% no of times that every nauron will be recalculated
% (neurons are recalculated in random permutation)
learning_iterations = 500;

%% calculating helpers
no_of_roads = size(training_data, 1);

probe_size = no_of_roads * (backward_data_usage_depth + 1);

%% creating 'random' weights vector
% weights vector is rand in range [0, 2/probe_size]
w = 2 / probe_size * rand(probe_size, 1);

%% creating probes from training data
% each column is 1 input point, each for loop iteration adds 1 column
probes = generate_probes(training_data, backward_data_usage_depth);

%% getting training_results arr from training data
training_results = training_data(road_you_need_speed_on, backward_data_usage_depth+1+how_far_forward_to_forecast:end)';

%% network learning!
done = 0;
for iterations = 1:learning_iterations*probe_size;
    w = make_learning_step(w, probes, training_results, how_far_forward_to_forecast);
    
    new_done = round(iterations*100/(learning_iterations*probe_size));
    if(new_done ~= done)
       done = new_done;
       disp([num2str(done), '% done'])
    end
end

%% tests
predictions = make_predictions(w, probes);
abs_error = abs((predictions(1:end-how_far_forward_to_forecast) - training_results));

%% for real data
%real_data = fix_input_data(real_data);
%probes = generate_probes(real_data, backward_data_usage_depth);
%predictions = make_predictions(w, probes);
%training_results = real_data(road_you_need_speed_on, backward_data_usage_depth+1+how_far_forward_to_forecast:end)';
%abs_error = abs((predictions(1:end-how_far_forward_to_forecast) - training_results));

clf
subplot(2,1,1)
plot(predictions, 'r')
hold on
plot(training_results, 'b')

title(['wartosc aktualna i predykcja na ', num2str(minutes_per_step*how_far_forward_to_forecast), ' minut'])
legend('predykcja', 'wartosc aktualna')

subplot(2,1,2)
plot(abs_error);
title('blad bezwzgledny')





end
